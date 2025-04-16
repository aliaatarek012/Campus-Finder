using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _CampusFinderInfrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Universities_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_UniversityID",
                table: "Events",
                column: "UniversityID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
