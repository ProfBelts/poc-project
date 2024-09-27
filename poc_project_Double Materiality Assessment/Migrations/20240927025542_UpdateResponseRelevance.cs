using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResponseRelevance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.AddColumn<int>(
                name: "DraftId1",
                table: "ResponseRelevances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseRelevances_DraftId1",
                table: "ResponseRelevances",
                column: "DraftId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "DraftId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId1",
                table: "ResponseRelevances",
                column: "DraftId1",
                principalTable: "Drafts",
                principalColumn: "DraftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId1",
                table: "ResponseRelevances");

            migrationBuilder.DropIndex(
                name: "IX_ResponseRelevances_DraftId1",
                table: "ResponseRelevances");

            migrationBuilder.DropColumn(
                name: "DraftId1",
                table: "ResponseRelevances");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "DraftId");
        }
    }
}
