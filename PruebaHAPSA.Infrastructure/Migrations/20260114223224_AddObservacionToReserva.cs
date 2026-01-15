using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaHAPSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddObservacionToReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Reservas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Reservas");
        }
    }
}
