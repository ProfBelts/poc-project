using Microsoft.AspNetCore.Mvc;
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
        private async Task<IActionResult> SubmitQuestionnaire(DoubleMaterialityViewModel model)
        {
            Console.WriteLine("SubmitQuestionnaire method hit"); // Debugging line

            if (ModelState.IsValid)
            {
                //// Save Stakeholder
                //dbContext.Stakeholders.Add(model.Stakeholder);
                //await dbContext.SaveChangesAsync(); // Save changes to get StakeholderId

                //// Save Responses
                //foreach (var response in model.Responses)
                //{
                //    if (response.RelevanceScore > 0) // Only save responses with a score
                //    {
                //        response.StakeholderId = model.Stakeholder.StakeholderId; // Link to the Stakeholder
                //        dbContext.ResponseRelevances.Add(response);
                //    }
                //}

                //await dbContext.SaveChangesAsync(); // Save responses

                return RedirectToAction("Index", "Home"); // Redirect to a success page or action
            }

            // If model state is invalid, return the view with the model
            return View("Questionnaire", model);
        }


    }
}
