using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class AddDraftEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    DraftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StakeholderId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    RelevanceScore = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialIssueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.DraftId);
                    table.ForeignKey(
                        name: "FK_Drafts_MaterialIssues_MaterialIssueId",
                        column: x => x.MaterialIssueId,
                        principalTable: "MaterialIssues",
                        principalColumn: "MaterialIssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drafts_Stakeholders_StakeholderId",
                        column: x => x.StakeholderId,
                        principalTable: "Stakeholders",
                        principalColumn: "StakeholderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_MaterialIssueId",
                table: "Drafts",
                column: "MaterialIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_StakeholderId",
                table: "Drafts",
                column: "StakeholderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drafts");
        }
    }
}
