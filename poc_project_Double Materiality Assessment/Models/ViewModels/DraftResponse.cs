using poc_project_Double_Materiality_Assessment.Models.Entities;

namespace poc_project_Double_Materiality_Assessment.Models.ViewModels
{
    public class DraftResponse
    {
        public int DraftId { get; set; }
        public int StakeholderId { get; set; }
        public string StakeholderName { get; set; }
        public string Organization { get; set; }
        public int IssueId { get; set; }
        public string IssueName { get; set; }
        public int RelevanceScore { get; set; }
        public string Comments { get; set; }
    }
}
