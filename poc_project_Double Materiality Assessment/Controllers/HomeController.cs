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
        public async Task<IActionResult> SubmitQuestionnaire(DoubleMaterialityViewModel model)
        {
            //// Print Stakeholder data
            //Console.WriteLine("Stakeholder Name: " + model.Stakeholder.Name);
            //Console.WriteLine("Organization: " + model.Stakeholder.Organization);
            //Console.WriteLine("Role: " + model.Stakeholder.Role);
            //Console.WriteLine("Category: " + model.Stakeholder.Category);


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
                return RedirectToAction("Thankyou");
            
        }

    

    public IActionResult ThankYou()
        {
            // Deserialize issue relevance from TempData
            //var issueRelevanceJson = TempData["IssueRelevance"] as string;
            //var issueRelevance = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, int>>(issueRelevanceJson);

            //// You can also access other TempData values here
            //ViewBag.StakeholderName = TempData["StakeholderName"];
            //ViewBag.Organization = TempData["Organization"];
            //ViewBag.Role = TempData["Role"];
            //ViewBag.Category = TempData["Category"];
            //ViewBag.AdditionalIssues = TempData["AdditionalIssues"];

            return View();
        }
    }
}
