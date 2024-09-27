using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_project_Double_Materiality_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances");

            migrationBuilder.AlterColumn<int>(
                name: "DraftId",
                table: "ResponseRelevances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "DraftId",
                table: "ResponseRelevances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseRelevances_Drafts_DraftId",
                table: "ResponseRelevances",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "DraftId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
