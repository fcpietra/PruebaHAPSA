using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Domain.Entities;

public abstract class Reserva
{
    public int Id { get; protected set; }
    public string Nombre { get; protected set; }
    public string Email { get; protected set; }
    public DateTime FechaHora { get; protected set; }
    public int CantidadPersonas { get; protected set; }
    public EstadoReserva Estado { get; protected set; }
    public string? Observacion { get; protected set; }

    protected static void ValidarComunes(string nombre, string email, DateTime fechaHora)
    {
        if (fechaHora < DateTime.Now)
            throw new ArgumentException("No se pueden crear reservas en el pasado.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("El formato del email no es válido.");
    }
    public void Confirmar()
    {
        if (Estado != EstadoReserva.Pendiente)
        {
            if (Estado == EstadoReserva.Confirmada) return;

            throw new InvalidOperationException($"No se puede confirmar una reserva en estado {Estado}.");
        }

        if (FechaHora < DateTime.Now)
        {
            throw new InvalidOperationException("No se puede confirmar una reserva que ya venció.");
        }

        Estado = EstadoReserva.Confirmada;
    }
    public void Cancelar(string motivo)
    {
        if (Estado == EstadoReserva.NoAsistio)
        {
            throw new InvalidOperationException("No se puede cancelar una reserva marcada como 'No Asistió'.");
        }
        if (Estado == EstadoReserva.Cancelada)
        {
            throw new InvalidOperationException("La reserva ya se encuentra cancelada.");
        }

        if (string.IsNullOrWhiteSpace(motivo))
        {
            throw new ArgumentException("Es obligatorio indicar el motivo de la cancelación.");
        }

        Estado = EstadoReserva.Cancelada;
        Observacion = motivo;
    }
    public void MarcarNoAsistio()
    {
        if (Estado != EstadoReserva.Confirmada)
        {
            throw new InvalidOperationException("Solo se puede marcar como 'No Asistió' una reserva previamente confirmada.");
        }

        Estado = EstadoReserva.NoAsistio;
    }
}