using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinaria.Core.Migrations
{
    /// <inheritdoc />
    public partial class RolPredeterminado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolUser_IdUser",
                schema: "security",
                table: "RolUser");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_IdUser_IdRol",
                schema: "security",
                table: "RolUser",
                columns: new[] { "IdUser", "IdRol" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolUser_IdUser_IdRol",
                schema: "security",
                table: "RolUser");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_IdUser",
                schema: "security",
                table: "RolUser",
                column: "IdUser");
        }
    }
}
