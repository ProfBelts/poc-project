using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraftResponses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DraftResponses",
                columns: table => new
                {
                    DraftResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DraftId = table.Column<int>(type: "int", nullable: false),
                    MaterialIssueId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    RelevanceScore = table.Column<int>(type: "int", nullable: false)
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
    }
}
