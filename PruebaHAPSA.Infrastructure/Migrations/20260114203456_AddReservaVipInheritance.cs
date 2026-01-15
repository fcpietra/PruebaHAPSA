using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaHAPSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReservaVipInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoVip",
                table: "Reservas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MesaPreferida",
                table: "Reservas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoReserva",
                table: "Reservas",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoVip",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "MesaPreferida",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "TipoReserva",
                table: "Reservas");
        }
    }
}
