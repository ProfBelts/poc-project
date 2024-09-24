using poc_project_Double_Materiality_Assessment.Models.Entities;

namespace poc_project_Double_Materiality_Assessment.Models.ViewModels
{
    public class DoubleMaterialityViewModel
    {
        public Stakeholder Stakeholder { get; set; } = new Stakeholder();
        public List<ResponseRelevance> Responses { get; set; } = new List<ResponseRelevance>();
        public List<MaterialIssue> MaterialIssues { get; set; } 
    }
}