uusing Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "DraftId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "DraftId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
