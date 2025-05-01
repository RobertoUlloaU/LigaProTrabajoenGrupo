using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LigaProTrabajoenGrupo.Models
{
    public class FormularioPartido
    {
        [Key]
        public int FormularioPartidoId { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio.")]
        public string NombreEquipo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos jugados debe ser un valor positivo.")]
        public int PartidosJugados { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos ganados debe ser un valor positivo.")]
        public int PartidosGanados { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos empatados debe ser un valor positivo.")]
        public int PartidosEmpatados { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos perdidos debe ser un valor positivo.")]
        public int PartidosPerdidos { get; set; }


        [ForeignKey("Equipo")]
        public int EquipoId { get; set; }

        // Relacion con el modelo Equipo
        public Equipo Equipo { get; set; }
    }
}
