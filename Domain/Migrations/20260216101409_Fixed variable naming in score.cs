using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Fixedvariablenaminginscore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Benches_BenchId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Benches_Scores_ScoreId",
                table: "Benches");

            migrationBuilder.DropForeignKey(
                name: "FK_Benches_Users_UserId",
                table: "Benches");

            migrationBuilder.DropIndex(
                name: "IX_Benches_ScoreId",
                table: "Benches");

            migrationBuilder.DropIndex(
                name: "IX_Benches_UserId",
                table: "Benches");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Benches");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Benches");

            migrationBuilder.DropTable(
                name: "Benches");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Scores",
                newName: "Average");

            migrationBuilder.CreateTable(
                name: "Benches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoreId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Benches_Scores_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "Scores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Benches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benches_ScoreId",
                table: "Benches",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Benches_UserId",
                table: "Benches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Benches_BenchId",
                table: "Photos",
                column: "BenchId",
                principalTable: "Benches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Benches_BenchId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Benches");

            migrationBuilder.RenameColumn(
                name: "Average",
                table: "Scores",
                newName: "Total");

            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Benches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Benches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Benches_ScoreId",
                table: "Benches",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Benches_UserId",
                table: "Benches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Benches_BenchId",
                table: "Photos",
                column: "BenchId",
                principalTable: "Benches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Benches_Scores_ScoreId",
                table: "Benches",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Benches_Users_UserId",
                table: "Benches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
