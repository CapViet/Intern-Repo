using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagerMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Scores",
            columns: table => new
            {
                ID = table.Column<int>(type: "int", nullable: false),
                MathScore = table.Column<int>(type: "int", nullable: false),
                ChemScore = table.Column<int>(type: "int", nullable: false),
                PhysScore = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Scores", x => x.ID);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Scores");
        }
    }
}
