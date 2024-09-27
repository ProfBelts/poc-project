﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Draft> Drafts { get; set; } = default!;

        public DbSet<DraftResponseRelevance> DraftsResponse { get; set; } = default!;


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

            modelBuilder.Entity<Draft>()
                .HasKey(d => d.DraftId);

            modelBuilder.Entity<DraftResponseRelevance>()
                .HasKey(dr => dr.ResponseId);

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


            modelBuilder.Entity<Draft>()
              .HasOne(d => d.Stakeholder) 
              .WithMany(s => s.Drafts) 
              .HasForeignKey(d => d.StakeholderId) 
              .OnDelete(DeleteBehavior.Cascade);




            base.OnModelCreating(modelBuilder);

        }

    }
}
