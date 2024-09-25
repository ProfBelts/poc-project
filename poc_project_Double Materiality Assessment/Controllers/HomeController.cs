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
            var allIssues = dbContext.MaterialIssues.ToList();
            return View(allIssues);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Questionnaire()
        {
            // Fetch all material issues
            var allIssues = dbContext.MaterialIssues.ToList();

            // Create a new view model instance
            var viewModel = new DoubleMaterialityViewModel
            {
                Stakeholder = new Stakeholder(), // Initialize an empty Stakeholder object
                MaterialIssues = allIssues,       // Pass the list of material issues
                Responses = new List<ResponseRelevance>() // Initialize the Responses list
            };

            // Initialize Responses with empty ResponseRelevance objects for each issue
            foreach (var issue in allIssues)
            {
                viewModel.Responses.Add(new ResponseRelevance
                {
                    IssueId = issue.MaterialIssueId // Link each ResponseRelevance to its MaterialIssue
                });
            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> QuestionnaireSubmit(DoubleMaterialityViewModel model)
        {

            //// Print the Responses
            //foreach (var response in model.Responses)
            //{
            //    Console.WriteLine("Relevance Score for Issue " + response.IssueId + ": " + response.RelevanceScore);
            //    Console.WriteLine("Comments: " + response.Comments);
            //}


            // Initialize the RelevanceResponses collection if it's null
            if (model.Stakeholder.RelevanceResponses == null)
            {
                model.Stakeholder.RelevanceResponses = new List<ResponseRelevance>();
            }

            // Loop through each response and create the necessary ResponseRelevance entities
            foreach (var response in model.Responses)
            {
                if (response.RelevanceScore > 0) // Only save responses with a relevance score
                {
                    // Link the response to the Stakeholder
                    response.StakeholderId = model.Stakeholder.StakeholderId;
                    // Ensure that Comments is not null 
                    response.Comments = response.Comments ?? string.Empty;
                    // Add the responses to the Stakeholder
                    model.Stakeholder.RelevanceResponses.Add(response);
                }
            }

            // Add Stakeholder to the DbContext
            dbContext.Stakeholders.Add(model.Stakeholder);

            // Save the Stakeholder 
            await dbContext.SaveChangesAsync();

            // Redirect to the Thank You page after successful submission
            return RedirectToAction("Response");

        }
       
        public IActionResult Response()
        {
            var organization = HttpContext.Session.GetString("Organization");

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

            var stakeHolderOrganization = dbContext.Stakeholders.FirstOrDefault(r => r.Organization.ToLower() == organization.ToLower()); 


            if(stakeHolderOrganization == null)
            {
                return RedirectToAction("Questionnaire");
            }

            return RedirectToAction("Response");

 
        
        }

    }









   



}

