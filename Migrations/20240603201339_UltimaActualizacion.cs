using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prueba_Tecnica_Api.Migrations
{
    /// <inheritdoc />
    public partial class UltimaActualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "idUsuario", "estadoUsuario", "idRoles", "nombreUsuario", "password" },
                values: new object[,]
                {
                    { 1, null, 1, "Fernando", "123" },
                    { 2, null, 2, "Hernan", "123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "idUsuario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "idUsuario",
                keyValue: 2);
        }
    }
}
