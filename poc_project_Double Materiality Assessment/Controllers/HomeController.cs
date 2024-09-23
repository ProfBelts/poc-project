using Microsoft.AspNetCore.Mvc;
using poc_project_Double_Materiality_Assessment.Data;
using poc_project_Double_Materiality_Assessment.Models.Entities;
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
            // Sample list of strings
            var allIssues = dbContext.MaterialIssues.ToList();
            return View(allIssues);
        }

        public class StakeholderInfo
        {
            public string StakeholderName { get; set; }
            public string Organization { get; set; }
            public string Role { get; set; }
            public string Category { get; set; }
        }

        public IActionResult QuestionnaireSubmit(StakeholderInfo stakeholderInfo, Dictionary<int, int> issueRelevance, string additionalIssues)
        {

            // Store the stakeholder info in TempData
            //TempData["StakeholderName"] = stakeholderInfo.StakeholderName;
            //TempData["Organization"] = stakeholderInfo.Organization;
            //TempData["Role"] = stakeholderInfo.Role;
            //TempData["Category"] = stakeholderInfo.Category;
            //TempData["AdditionalIssues"] = additionalIssues;

            //// Store issue relevance scores in TempData
            //TempData["IssueRelevance"] = Newtonsoft.Json.JsonConvert.SerializeObject(issueRelevance);


            var stakeholder = new Stakeholder()
            {
                Name = stakeholderInfo.StakeholderName,
                Organization = stakeholderInfo.Organization,
                Role = stakeholderInfo.Role,
                Category = stakeholderInfo.Category
            };

            dbContext.Stakeholders.Add(stakeholder);

            dbContext.SaveChanges();


            return RedirectToAction("ThankYou");


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
