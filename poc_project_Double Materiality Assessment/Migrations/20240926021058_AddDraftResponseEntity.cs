using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class AddDraftResponseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drafts_MaterialIssues_MaterialIssueId",
                table: "Drafts");

            migrationBuilder.DropIndex(
                name: "IX_Drafts_MaterialIssueId",
                table: "Drafts");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Drafts");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Drafts");

            migrationBuilder.DropColumn(
                name: "MaterialIssueId",
                table: "Drafts");

            migrationBuilder.DropColumn(
                name: "RelevanceScore",
                table: "Drafts");

            migrationBuilder.CreateTable(
                name: "DraftResponses",
                columns: table => new
                {
                    DraftResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DraftId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    RelevanceScore = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialIssueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftResponses", x => x.DraftResponseId);
                    table.ForeignKey(
                        name: "FK_DraftResponses_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "DraftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DraftResponses_MaterialIssues_MaterialIssueId",
                        column: x => x.MaterialIssueId,
                        principalTable: "MaterialIssues",
                        principalColumn: "MaterialIssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DraftResponses_DraftId",
                table: "DraftResponses",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftResponses_MaterialIssueId",
                table: "DraftResponses",
                column: "MaterialIssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraftResponses");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Drafts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "Drafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialIssueId",
                table: "Drafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RelevanceScore",
                table: "Drafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_MaterialIssueId",
                table: "Drafts",
                column: "MaterialIssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drafts_MaterialIssues_MaterialIssueId",
                table: "Drafts",
                column: "MaterialIssueId",
                principalTable: "MaterialIssues",
                principalColumn: "MaterialIssueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
