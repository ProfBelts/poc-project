using Microsoft.EntityFrameworkCore;
using poc_project_Double_Materiality_Assessment.Models.Entities;

namespace poc_project_Double_Materiality_Assessment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Stakeholder> Stakeholders { get; set; } = default!;
        public DbSet<MaterialIssue> MaterialIssues { get; set; } = default!;
        public DbSet<ResponsePriority> ResponsePriorities { get; set; } = default!;
        public DbSet<ResponseRelevance> ResponseRelevances { get; set; } = default!;
        public DbSet<AdditionalIssue> AdditionalIssues { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ResponseRelevance>()
             .HasOne(rr => rr.Stakeholder)
             .WithMany(s => s.RelevanceResponses)
             .HasForeignKey(rr => rr.StakeholderId);

            // ResponseRelevance to MaterialIssue relationship
            modelBuilder.Entity<ResponseRelevance>()
                .HasOne(rr => rr.Issue)
                .WithMany()
                .HasForeignKey(rr => rr.IssueId);

            modelBuilder.Entity<ResponseRelevance>()
                .HasKey(rr => rr.ResponseId);

            modelBuilder.Entity<AdditionalIssue>()
                .HasKey(ai => ai.IssueId);

            // Similar relationships for ResponsePriority
            modelBuilder.Entity<ResponsePriority>()
                .HasOne(rp => rp.Stakeholder)
                .WithMany()
                .HasForeignKey(rp => rp.StakeholderId);


            modelBuilder.Entity<ResponsePriority>()
                .HasOne(rp => rp.Issue)
                .WithMany()
                .HasForeignKey(rp => rp.IssueId);

            modelBuilder.Entity<ResponsePriority>()
                .HasKey(rp => rp.ResponseId);


            base.OnModelCreating(modelBuilder);

        }

    }
}
