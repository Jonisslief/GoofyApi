using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Madescoresone2one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BenchId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_BenchId",
                table: "Scores",
                column: "BenchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Benches_BenchId",
                table: "Scores",
                column: "BenchId",
                principalTable: "Benches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Benches_BenchId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_BenchId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "BenchId",
                table: "Scores");
        }
    }
}
