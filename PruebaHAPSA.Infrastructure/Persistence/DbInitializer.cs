using PruebaHAPSA.Domain.Entities;

namespace PruebaHAPSA.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(PruebaHapsaDbContext context)
    {
        // Asegurarse de que la BD exista
        context.Database.EnsureCreated();

        // Si ya hay reservas, no hacemos nada
        if (context.Reservas.Any())
        {
            return; 
        }

        // --- CREAMOS DATOS FAKE QUE RESPETEN TUS REGLAS DE NEGOCIO ---
        // Recordá que las validaciones del Dominio (Fechas futuras, Horarios) siguen vigentes.

        var reservas = new List<Reserva>();

        // 1. Reserva Estándar (Pendiente)
        // Horario permitido: 19:00 a 23:30
        var estandar = ReservaEstandar.Crear( // Ojo: Si no creaste la clase concreta Estandar, usá la lógica que tengas
            "Facundo Dev",
            "facundo@dev.com",
            DateTime.Now.AddDays(1).Date.AddHours(20), // Mañana a las 20:00
            4
        );
        reservas.Add(estandar);

        // 2. Reserva VIP (Confirmada)
        // Horario permitido: 12:00 a 01:00. Código > 6 chars.
        var vip = ReservaVip.Crear(
            "Ricardo Fort",
            "ricky@basta.com",
            DateTime.Now.AddDays(2).Date.AddHours(23), // Pasado mañana a las 23:00
            2,
            "MAIAMEEE", 
            "Mesa cerca del escenario"
        );
        reservas.Add(vip);

        // 3. Reserva Cumpleaños (Pendiente)
        // Mínimo 48hs antes si quiere torta. Mínimo 5 personas.
        var cumple = ReservaCumpleanos.Crear(
            "Lionel Messi",
            "lio@afa.com",
            DateTime.Now.AddDays(5).Date.AddHours(21), // Dentro de 5 días
            10,
            38,
            true // Quiere torta
        );
        reservas.Add(cumple);

        // 4. Una Cancelada para probar el color rojo en el front
        // Creamos una estándar y la cancelamos
        var cancelada = ReservaEstandar.Crear(
            "Usuario Arrepentido",
            "cancel@mail.com",
            DateTime.Now.AddDays(3).Date.AddHours(20),
            2
        );
        cancelada.Cancelar("Me surgió un imprevisto");
        reservas.Add(cancelada);

        // Guardamos todo de una
        context.Reservas.AddRange(reservas);
        context.SaveChanges();
    }
}