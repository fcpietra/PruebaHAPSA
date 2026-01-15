using Moq;
using NUnit.Framework;
using PruebaHAPSA.Application.DTOs;
using PruebaHAPSA.Application.Services;
using PruebaHAPSA.Domain.Entities;
using PruebaHAPSA.Domain.Interfaces;

namespace PruebaHAPSA.Tests.Application;

[TestFixture]
public class ReservaServiceTests
{
    private Mock<IReservaRepository> _repoMock;
    private ReservaService _service;

    [SetUp]
    public void Setup()
    {
        // 1. Creamos el Mock del repositorio
        _repoMock = new Mock<IReservaRepository>();

        // 2. Inyectamos el objeto "mentiroso" (_repoMock.Object) al servicio
        _service = new ReservaService(_repoMock.Object);
    }

    [Test]
    public async Task CrearReservaVip_CuandoHayLugar_DeberiaGuardarEnRepo()
    {
        // Arrange
        var dto = new CrearReservaVipDto(
            "Facundo",
            "test@mail.com",
            DateTime.Now.AddDays(1).Date.AddHours(22),
            2,
            "VIP123456",
            null
        );

        // Simulamos que el repositorio dice "Sí, hay lugar"
        _repoMock.Setup(r => r.HayDisponibilidadAsync(It.IsAny<DateTime>(), It.IsAny<int>()))
                 .ReturnsAsync(true);

        // Act
        var id = await _service.CrearReservaVipAsync(dto);

        // Assert
        // Verificamos que el servicio haya llamado al método AddAsync del repositorio 1 vez
        _repoMock.Verify(r => r.AddAsync(It.IsAny<ReservaVip>()), Times.Once);
    }

    [Test]
    public void CrearReservaVip_CuandoNoHayLugar_DeberiaLanzarExcepcion()
    {
        // Arrange
        var dto = new CrearReservaVipDto(
            "Facundo",
            "test@mail.com",
            DateTime.Now.AddDays(1).Date.AddHours(22),
            2,
            "VIP123456",
            null
        );

        // Simulamos que el repositorio dice "NO hay lugar" (False)
        _repoMock.Setup(r => r.HayDisponibilidadAsync(It.IsAny<DateTime>(), It.IsAny<int>()))
                 .ReturnsAsync(false);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CrearReservaVipAsync(dto)
        );

        Assert.That(ex.Message, Is.EqualTo("No hay mesas disponibles para ese horario."));

        // Verificamos que NUNCA se haya intentado guardar
        _repoMock.Verify(r => r.AddAsync(It.IsAny<ReservaVip>()), Times.Never);
    }
}