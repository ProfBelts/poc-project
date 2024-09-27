﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using poc_project_Double_Materiality_Assessment.Data;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240927030604_UpdateEntityResponse")]
    partial class UpdateEntityResponse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdditionalIssue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImportanceRank")
                        .HasColumnType("int");

                    b.Property<string>("IssueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StakeholderId")
                        .HasColumnType("int");

                    b.HasKey("IssueId");

                    b.HasIndex("StakeholderId");

                    b.ToTable("AdditionalIssues");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.Draft", b =>
                {
                    b.Property<int>("DraftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DraftId"));

                    b.Property<int>("StakeholderId")
                        .HasColumnType("int");

                    b.HasKey("DraftId");

                    b.HasIndex("StakeholderId");

                    b.ToTable("Drafts");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.MaterialIssue", b =>
                {
                    b.Property<int>("MaterialIssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialIssueId"));

                    b.Property<string>("IssueCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaterialIssueId");

                    b.ToTable("MaterialIssues");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.ResponsePriority", b =>
                {
                    b.Property<int>("ResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResponseId"));

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityRank")
                        .HasColumnType("int");

                    b.Property<int>("StakeholderId")
                        .HasColumnType("int");

                    b.HasKey("ResponseId");

                    b.HasIndex("IssueId");

                    b.HasIndex("StakeholderId");

                    b.ToTable("ResponsePriorities");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.ResponseRelevance", b =>
                {
                    b.Property<int>("ResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResponseId"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DraftId")
                        .HasColumnType("int");

                    b.Property<int?>("DraftId1")
                        .HasColumnType("int");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("RelevanceScore")
                        .HasColumnType("int");

                    b.Property<int>("StakeholderId")
                        .HasColumnType("int");

                    b.HasKey("ResponseId");

                    b.HasIndex("DraftId");

                    b.HasIndex("DraftId1");

                    b.HasIndex("IssueId");

                    b.HasIndex("StakeholderId");

                    b.ToTable("ResponseRelevances");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.Stakeholder", b =>
                {
                    b.Property<int>("StakeholderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StakeholderId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StakeholderId");

                    b.ToTable("Stakeholders");
                });

            modelBuilder.Entity("AdditionalIssue", b =>
                {
                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.Stakeholder", "Stakeholder")
                        .WithMany()
                        .HasForeignKey("StakeholderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stakeholder");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.Draft", b =>
                {
                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.Stakeholder", "Stakeholder")
                        .WithMany("Drafts")
                        .HasForeignKey("StakeholderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stakeholder");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.ResponsePriority", b =>
                {
                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.MaterialIssue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.Stakeholder", "Stakeholder")
                        .WithMany()
                        .HasForeignKey("StakeholderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");

                    b.Navigation("Stakeholder");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.ResponseRelevance", b =>
                {
                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.Draft", "Draft")
                        .WithMany()
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.Draft", null)
                        .WithMany("RelevanceResponses")
                        .HasForeignKey("DraftId1");

                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.MaterialIssue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("poc_project_Double_Materiality_Assessment.Models.Entities.Stakeholder", "Stakeholder")
                        .WithMany("RelevanceResponses")
                        .HasForeignKey("StakeholderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Draft");

                    b.Navigation("Issue");

                    b.Navigation("Stakeholder");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.Draft", b =>
                {
                    b.Navigation("RelevanceResponses");
                });

            modelBuilder.Entity("poc_project_Double_Materiality_Assessment.Models.Entities.Stakeholder", b =>
                {
                    b.Navigation("Drafts");

                    b.Navigation("RelevanceResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
