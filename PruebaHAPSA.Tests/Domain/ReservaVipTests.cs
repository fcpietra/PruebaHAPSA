using NUnit.Framework;
using PruebaHAPSA.Domain.Entities;
using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Tests.Domain;

[TestFixture]
public class ReservaVipTests
{
    [Test]
    public void Crear_ConDatosValidos_DeberiaRetornarReservaConfirmada()
    {
        // Arrange (Preparar)
        var fechaValida = DateTime.Now.AddDays(1).Date.AddHours(20); // 20:00 hs

        // Act (Actuar)
        // Nota: Asegurate de usar una hora válida para VIP (12:00 a 01:00)
        var reserva = ReservaVip.Crear(
            "Facundo",
            "facu@mail.com",
            fechaValida,
            2,
            "CODIGOVIP123",
            "Ventana"
        );

        // Assert (Verificar)
        Assert.Multiple(() =>
        {
            Assert.That(reserva.Estado, Is.EqualTo(EstadoReserva.Confirmada));
            Assert.That(reserva.Nombre, Is.EqualTo("Facundo"));
        });
    }

    [Test]
    public void Crear_ConCodigoCorto_DeberiaLanzarExcepcion()
    {
        // Arrange
        var fechaValida = DateTime.Now.AddDays(1);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ReservaVip.Crear("Facundo", "mail@test.com", fechaValida, 2, "123", null)
        );

        Assert.That(ex.Message, Does.Contain("Código VIP"));
    }
}