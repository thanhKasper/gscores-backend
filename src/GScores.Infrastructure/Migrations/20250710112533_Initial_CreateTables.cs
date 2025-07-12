using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GScores.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForeignLanguageCodes",
                columns: table => new
                {
                    ForeignCode = table.Column<string>(type: "varchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignLanguageCodes", x => x.ForeignCode);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MathScore = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    LiteratureScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    ForeignScore = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    IsNaturalScience = table.Column<bool>(type: "bit", nullable: false),
                    HistoryScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    GeographyScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    CivicEducationScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    PhysicsScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    ChemistryScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    BiologyScore = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    ForeignLanguageCodeForeignCode = table.Column<string>(type: "varchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_ForeignLanguageCodes_ForeignLanguageCodeForeignCode",
                        column: x => x.ForeignLanguageCodeForeignCode,
                        principalTable: "ForeignLanguageCodes",
                        principalColumn: "ForeignCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ForeignLanguageCodeForeignCode",
                table: "Students",
                column: "ForeignLanguageCodeForeignCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ForeignLanguageCodes");
        }
    }
}
