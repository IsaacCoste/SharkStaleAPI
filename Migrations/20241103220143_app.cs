using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharkStyleApi.Migrations
{
    /// <inheritdoc />
    public partial class app : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagenn",
                table: "Categorias",
                newName: "Imagen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagen",
                table: "Categorias",
                newName: "Imagenn");
        }
    }
}
