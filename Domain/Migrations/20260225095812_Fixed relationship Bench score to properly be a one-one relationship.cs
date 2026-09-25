using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixedrelationshipBenchscoretoproperlybeaoneonerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benches_Scores_ScoreId",
                table: "Benches");

            migrationBuilder.DropIndex(
                name: "IX_Scores_BenchId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Benches_ScoreId",
                table: "Benches");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Benches");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_BenchId",
                table: "Scores",
                column: "BenchId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Scores_BenchId",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Benches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_BenchId",
                table: "Scores",
                column: "BenchId");

            migrationBuilder.CreateIndex(
                name: "IX_Benches_ScoreId",
                table: "Benches",
                column: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benches_Scores_ScoreId",
                table: "Benches",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
