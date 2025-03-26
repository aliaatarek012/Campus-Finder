using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _CampusFinderInfrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ResyncModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EnglishTests",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishTests", x => x.TestID);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RequiredDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LearningStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniPhone = table.Column<int>(type: "int", nullable: true),
                    PrimaryLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.UniversityID);
                });

            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    CollegeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.CollegeID);
                    table.ForeignKey(
                        name: "FK_Colleges_Universities_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollegeDiplomas",
                columns: table => new
                {
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    DiplomaID = table.Column<int>(type: "int", nullable: false),
                    Min_Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeDiplomas", x => new { x.CollegeID, x.DiplomaID });
                    table.ForeignKey(
                        name: "FK_CollegeDiplomas_Colleges_CollegeID",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegeDiplomas_Diplomas_DiplomaID",
                        column: x => x.DiplomaID,
                        principalTable: "Diplomas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollegeEnglishTests",
                columns: table => new
                {
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    Min_Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeEnglishTests", x => new { x.CollegeID, x.TestID });
                    table.ForeignKey(
                        name: "FK_CollegeEnglishTests_Colleges_CollegeID",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegeEnglishTests_EnglishTests_TestID",
                        column: x => x.TestID,
                        principalTable: "EnglishTests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorID);
                    table.ForeignKey(
                        name: "FK_Majors_Colleges_CollegeID",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollegeDiplomas_CollegeID",
                table: "CollegeDiplomas",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeDiplomas_DiplomaID",
                table: "CollegeDiplomas",
                column: "DiplomaID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeEnglishTests_CollegeID",
                table: "CollegeEnglishTests",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeEnglishTests_TestID",
                table: "CollegeEnglishTests",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Colleges_UniversityID",
                table: "Colleges",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_CollegeID",
                table: "Majors",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Location",
                table: "Universities",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Name",
                table: "Universities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Rank",
                table: "Universities",
                column: "Rank");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegeDiplomas");

            migrationBuilder.DropTable(
                name: "CollegeEnglishTests");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "EnglishTests");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
