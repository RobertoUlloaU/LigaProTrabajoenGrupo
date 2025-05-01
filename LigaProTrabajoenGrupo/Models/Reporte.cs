using System.ComponentModel.DataAnnotations;

namespace LigaProTrabajoenGrupo.Models
{
    public class Reporte
    {
        [Key]
        public int ReporteId { get; set; }

        // Tabla de los 5 jugadores con mas goles
        public List<Jugador> JugadoresConMasGoles { get; set; }

        // Tabla de los 5 jugadores con mas asistencias
        public List<Jugador> JugadoresConMasAsistencias { get; set; }

        // Tabla de los 5 equipos con mayor presupuesto
        public List<Equipo> EquiposConMayorPresupuesto { get; set; }
    }
}
