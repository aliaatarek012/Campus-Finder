using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _CampusFinderInfrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddYearsOFDurationColumntoColleges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YearsOfDration",
                table: "Colleges",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOfDration",
                table: "Colleges");
        }
    }
}
