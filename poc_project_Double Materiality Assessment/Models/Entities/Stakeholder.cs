namespace poc_project_Double_Materiality_Assessment.Models.Entities
{
    public class Stakeholder
    {
        public int StakeholderId { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }
        public string Category { get; set; }
        public ICollection<ResponseRelevance> RelevanceResponses { get; set; }
    }

}
