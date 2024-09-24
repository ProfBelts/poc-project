namespace poc_project_Double_Materiality_Assessment.Models.Entities
{
    public class ResponseRelevance
    {
        public int ResponseId { get; set; }
        public int StakeholderId { get; set; }
        public Stakeholder Stakeholder { get; set; }
        public int IssueId { get; set; }
        public MaterialIssue Issue { get; set; }
        public int RelevanceScore { get; set; }
        public string Comments { get; set; }
    }
}
