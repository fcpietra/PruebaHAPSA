using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaHAPSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReservaCumpleanos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EdadCumpleanero",
                table: "Reservas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereTorta",
                table: "Reservas",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EdadCumpleanero",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "RequiereTorta",
                table: "Reservas");
        }
    }
}
