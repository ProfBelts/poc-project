using poc_project_Double_Materiality_Assessment.Models.Entities;

public class AdditionalIssue
{
    public int IssueId { get; set; }
    public int StakeholderId { get; set; }
    public Stakeholder Stakeholder { get; set; }
    public string IssueName { get; set; }
    public int ImportanceRank { get; set; }
    public string Comments { get; set; }
}