using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class AddRelevanceResponseToDraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DraftId",
                table: "ResponseRelevances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseRelevances_DraftId",
                table: "ResponseRelevances",
                column: "DraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "DraftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.DropIndex(
                name: "IX_ResponseRelevances_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.DropColumn(
                name: "DraftId",
                table: "ResponseRelevances");
        }
    }
}
