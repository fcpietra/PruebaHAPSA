using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Domain.Entities;

public class ReservaCumpleanos : Reserva
{
    public int EdadCumpleanero { get; private set; }
    public bool RequiereTorta { get; private set; }

    private ReservaCumpleanos() { }

    public static ReservaCumpleanos Crear(
        string nombre,
        string email,
        DateTime fechaHora,
        int cantidadPersonas,
        int edadCumpleanero,
        bool requiereTorta)
    {
        // 1. Validaciones Comunes (No fechas pasadas, email válido, nombre) - Criterios 5 y 6
        ValidarComunes(nombre, email, fechaHora);

        // 2. Validación Cantidad de Personas: 5 a 12 (Criterio 2)
        if (cantidadPersonas < 5 || cantidadPersonas > 12)
            throw new ArgumentException("Para cumpleaños, la cantidad de personas debe ser entre 5 y 12.");

        // 3. Validación Torta: Mínimo 48 horas de anticipación (Criterio 3)
        if (requiereTorta)
        {
            if (fechaHora < DateTime.Now.AddHours(48))
                throw new ArgumentException("Si requiere torta, debe reservar con al menos 48 horas de anticipación.");
        }

        // 4. Validación Horario: Hasta las 23:00 (Criterio 4)
        // Interpretamos "Hasta las 23:00" como hora de inicio máxima permitida.
        var horaLimite = new TimeSpan(23, 0, 0);
        if (fechaHora.TimeOfDay > horaLimite)
            throw new ArgumentException("El horario límite para iniciar cumpleaños es a las 23:00.");

        // Validar edad lógica (opcional, pero sentido común)
        if (edadCumpleanero < 0 || edadCumpleanero > 120)
            throw new ArgumentException("La edad ingresada no es válida.");

        // 5. Crear instancia con estado Pendiente (Criterio 8)
        return new ReservaCumpleanos
        {
            Nombre = nombre,
            Email = email,
            FechaHora = fechaHora,
            CantidadPersonas = cantidadPersonas,
            Estado = EstadoReserva.Pendiente,
            EdadCumpleanero = edadCumpleanero,
            RequiereTorta = requiereTorta
        };
    }
}