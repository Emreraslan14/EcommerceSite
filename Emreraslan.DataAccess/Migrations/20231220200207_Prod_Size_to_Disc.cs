using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emreraslan.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Prod_Size_to_Disc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Products",
                newName: "Discount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Products",
                newName: "Size");
        }
    }
}
