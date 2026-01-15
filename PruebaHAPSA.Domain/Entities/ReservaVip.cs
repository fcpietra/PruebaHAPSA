using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Domain.Entities;

public class ReservaVip : Reserva
{
    public string CodigoVip { get; private set; }
    public string? MesaPreferida { get; private set; } // Opcional

    private ReservaVip() { } 

    public static ReservaVip Crear(string nombre, string email, DateTime fechaHora, int cantidadPersonas, string codigoVip, string? mesaPreferida)
    {
        // 1. Validaciones Comunes
        ValidarComunes(nombre, email, fechaHora);

        // 2. Validación VIP Code (AC 2)
        if (string.IsNullOrWhiteSpace(codigoVip) || codigoVip.Length < 6)
            throw new ArgumentException("El Código VIP debe tener al menos 6 caracteres.");

        // 3. Validación Horario: 12:00 a 01:00 (AC 3)
        // Nota: 01:00 implica que puede ser la madrugada del día siguiente, 
        // pero simplifiquemos validando la "hora del reloj".
        var hora = fechaHora.TimeOfDay;
        var inicio = new TimeSpan(12, 0, 0); // 12:00
        var fin = new TimeSpan(1, 0, 0);     // 01:00 AM

        // Lógica: Es válido si es mayor a las 12:00 O menor a la 01:00
        bool esHorarioValido = (hora >= inicio) || (hora <= fin);

        if (!esHorarioValido)
            throw new ArgumentException("El horario VIP permitido es entre 12:00 y 01:00.");

        // 4. Crear instancia (AC 8: Estado Confirmada)
        return new ReservaVip
        {
            Nombre = nombre,
            Email = email,
            FechaHora = fechaHora,
            CantidadPersonas = cantidadPersonas,
            Estado = EstadoReserva.Confirmada,
            CodigoVip = codigoVip,
            MesaPreferida = mesaPreferida
        };
    }
}