namespace poc_project_Double_Materiality_Assessment.Models.Entities
{
    public class Draft
    {
        public int DraftId { get; set; }
        public int StakeholderId { get; set; }
        public ICollection<ResponseRelevance> RelevanceResponses { get; set; } = new List<ResponseRelevance>();

        // Navigation property for Stakeholder
        public Stakeholder Stakeholder { get; set; }



    }


}
