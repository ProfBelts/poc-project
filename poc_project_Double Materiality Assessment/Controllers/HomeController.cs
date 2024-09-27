using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poc_project_Double_Materiality_Assessment.Data;
using poc_project_Double_Materiality_Assessment.Models.Entities;
using poc_project_Double_Materiality_Assessment.Models.ViewModels;
using System.Diagnostics;

namespace poc_project_Double_Materiality_Assessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        // Inject both ILogger and ApplicationDbContext in the constructor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;

        }

        // The Index action fetches all material issues and passes them to the view
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Questionnaire()
        {
            // Get the organization from the session
            var organization = HttpContext.Session.GetString("Organization");

            if (organization == null)
            {
                return RedirectToAction("Index");
            }

            // Fetch the stakeholder based on the organization
            var stakeholder = dbContext.Stakeholders
                .FirstOrDefault(s => s.Organization == organization);

            if (stakeholder == null)
            {
                // If stakeholder does not exist, create a new one
                stakeholder = new Stakeholder
                {
                    Organization = organization
                };
            }

            var allIssues = dbContext.MaterialIssues.ToList();

            var draftResponses = from d in dbContext.Drafts
                                 join s in dbContext.Stakeholders on d.StakeholderId equals s.StakeholderId
                                 join rr in dbContext.ResponseRelevances on s.StakeholderId equals rr.StakeholderId
                                 join m in dbContext.MaterialIssues on rr.IssueId equals m.MaterialIssueId
                                 where s.Organization == stakeholder.Organization
                                 select new
                                 {
                                     IssueId = m.MaterialIssueId,
                                     Name = s.Name,
                                     Organization = s.Organization,
                                     Role = s.Role,
                                     Category = s.Category,
                                     Comments = rr.Comments,
                                     RelevanceScore = rr.RelevanceScore
                                 };

            var viewModel = new DoubleMaterialityViewModel
            {
                Stakeholder = stakeholder,
                MaterialIssues = allIssues,
                Responses = new List<ResponseRelevance>()
            };

            // Populate the responses with draft responses or create empty ones
            foreach (var issue in allIssues)
            {
                var draftResponse = draftResponses.FirstOrDefault(dr => dr.IssueId == issue.MaterialIssueId);

                viewModel.Responses.Add(new ResponseRelevance
                {
                    IssueId = issue.MaterialIssueId,
                    RelevanceScore = draftResponse?.RelevanceScore ?? 0, // Use draft response if it exists
                    Comments = draftResponse?.Comments ?? string.Empty // Use draft response comments if it exists
                });
            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> QuestionnaireSubmit(DoubleMaterialityViewModel model)
        {

            // Initialize the RelevanceResponses collection if it's null
            if (model.Stakeholder.RelevanceResponses == null)
            {
                model.Stakeholder.RelevanceResponses = new List<ResponseRelevance>();
            }

            foreach (var response in model.Responses)
            {
                if (response.RelevanceScore > 0) 
                {
                
                    response.StakeholderId = model.Stakeholder.StakeholderId;
                 
                    response.Comments = response.Comments ?? string.Empty;
               
                    model.Stakeholder.RelevanceResponses.Add(response);
                }
            }
    
            dbContext.Stakeholders.Add(model.Stakeholder);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("Response");

        }

        public IActionResult Response()
        {
            var organization = HttpContext.Session.GetString("Organization");

            if (organization == null)
            {
                return RedirectToAction("Index");
            }


            var responses = dbContext.ResponseRelevances
            .Join(dbContext.Stakeholders,
                  rr => rr.StakeholderId,
                  s => s.StakeholderId,
                  (rr, s) => new { rr, s })
            .Join(dbContext.MaterialIssues,
                  combined => combined.rr.IssueId,
                  m => m.MaterialIssueId,
                  (combined, m) => new ResponseViewModel
                  {
                      Name = combined.s.Name,
                      Organization = combined.s.Organization,
                      Role = combined.s.Role,
                      Category = combined.s.Category,
                      RelevanceScore = combined.rr.RelevanceScore,
                      Comments = combined.rr.Comments,
                      IssueName = m.IssueName,
                      IssueCategory = m.IssueCategory
                  })
             .Where(response => response.Organization == organization) // Filter by organization
            .GroupBy(response => new { response.IssueName, response.IssueCategory }) // Group by issue name and category
            .Select(g => new ResponseViewModel
            {
                Name = g.FirstOrDefault().Name,
                Organization = g.FirstOrDefault().Organization,
                Role = g.FirstOrDefault().Role,
                Category = g.FirstOrDefault().Category,
                RelevanceScore = (int)g.Average(x => x.RelevanceScore), // Calculate the average relevance score
                Comments = string.Join("<br/>", g
                .Where(x => !string.IsNullOrEmpty(x.Comments))
                .Select(x => $"{x.Name} ({x.Role}): {x.Comments}")),
                IssueName = g.Key.IssueName,
                IssueCategory = g.Key.IssueCategory
            })
        .OrderByDescending(response => response.RelevanceScore) // Sort by average RelevanceScore descending
        .ToList();


            return View(responses);
        }

        [HttpPost]
        public IActionResult CheckOrganization(string organization)
        {
            // Store the variable in session
            HttpContext.Session.SetString("Organization", organization);

            var stakeHolderOrganization = dbContext.Stakeholders.FirstOrDefault(r => r.Organization == organization);

            var draft = dbContext.Drafts.FirstOrDefault(d => d.Stakeholder.Organization == organization);


            if (stakeHolderOrganization == null)
            {
                return RedirectToAction("Questionnaire");
            }

            if(draft != null)
            {
                return RedirectToAction("Questionnaire");
            }

            return RedirectToAction("Response");

        }

        [HttpPost]
        public IActionResult SaveForLater(DoubleMaterialityViewModel model)
        {
            foreach (var response in model.Responses)
            {
                Console.WriteLine($"Relevance Score for Issue {response.IssueId}: {response.RelevanceScore}");
                Console.WriteLine($"Comments: {response.Comments}");
            }

            var existingStakeHolder = dbContext.Stakeholders.FirstOrDefault(s => s.StakeholderId == model.Stakeholder.StakeholderId);

            if (existingStakeHolder == null)
            {
                dbContext.Stakeholders.Add(model.Stakeholder);
                dbContext.SaveChanges();
            }

            var draft = new Draft
            {
                StakeholderId = model.Stakeholder.StakeholderId,
            };

            bool hasRelevantResponses = false;

            // Prepare to save the relevant responses
            foreach (var response in model.Responses)
            {
                if (response.RelevanceScore > 0)
                {
                    hasRelevantResponses = true;

                    // Create a new ResponseRelevance object
                    var relevanceResponse = new ResponseRelevance
                    {
                        StakeholderId = model.Stakeholder.StakeholderId,
                        IssueId = response.IssueId,
                        RelevanceScore = response.RelevanceScore,
                        Comments = response.Comments ?? string.Empty 
                    };

                    // Add the response to the Stakeholder's responses
                    draft.RelevanceResponses.Add(relevanceResponse);
                    dbContext.SaveChanges();
                }
            }

            if (hasRelevantResponses)
            {
                dbContext.Drafts.Add(draft);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}
















