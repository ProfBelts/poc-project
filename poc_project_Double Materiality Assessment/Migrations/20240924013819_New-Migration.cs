using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialIssues",
                columns: table => new
                {
                    MaterialIssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialIssues", x => x.MaterialIssueId);
                });

            migrationBuilder.CreateTable(
                name: "Stakeholders",
                columns: table => new
                {
                    StakeholderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stakeholders", x => x.StakeholderId);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalIssues",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StakeholderId = table.Column<int>(type: "int", nullable: false),
                    IssueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportanceRank = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalIssues", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK_AdditionalIssues_Stakeholders_StakeholderId",
                        column: x => x.StakeholderId,
                        principalTable: "Stakeholders",
                        principalColumn: "StakeholderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsePriorities",
                columns: table => new
                {
                    ResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StakeholderId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    PriorityRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsePriorities", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_ResponsePriorities_MaterialIssues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "MaterialIssues",
                        principalColumn: "MaterialIssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsePriorities_Stakeholders_StakeholderId",
                        column: x => x.StakeholderId,
                        principalTable: "Stakeholders",
                        principalColumn: "StakeholderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseRelevances",
                columns: table => new
                {
                    ResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StakeholderId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    RelevanceScore = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseRelevances", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_ResponseRelevances_MaterialIssues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "MaterialIssues",
                        principalColumn: "MaterialIssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseRelevances_Stakeholders_StakeholderId",
                        column: x => x.StakeholderId,
                        principalTable: "Stakeholders",
                        principalColumn: "StakeholderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalIssues_StakeholderId",
                table: "AdditionalIssues",
                column: "StakeholderId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsePriorities_IssueId",
                table: "ResponsePriorities",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsePriorities_StakeholderId",
                table: "ResponsePriorities",
                column: "StakeholderId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseRelevances_IssueId",
                table: "ResponseRelevances",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseRelevances_StakeholderId",
                table: "ResponseRelevances",
                column: "StakeholderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalIssues");

            migrationBuilder.DropTable(
                name: "ResponsePriorities");

            migrationBuilder.DropTable(
                name: "ResponseRelevances");

            migrationBuilder.DropTable(
                name: "MaterialIssues");

            migrationBuilder.DropTable(
                name: "Stakeholders");
        }
    }
}
