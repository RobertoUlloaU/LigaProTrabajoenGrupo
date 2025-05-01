using System.ComponentModel.DataAnnotations;

namespace LigaProTrabajoenGrupo.Models
{
    public class Equipo
    {
        [Key]
        public int EquipoId { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del equipo no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no puede superar los 255 caracteres.")]
        public string Descripcion { get; set; }

        [StringLength(255, ErrorMessage = "La URL del logo no puede superar los 255 caracteres.")]
        public string Logo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto debe ser un valor positivo.")]
        public double Presupuesto { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos jugados debe ser un valor positivo.")]
        public int PartidosJugados { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos ganados debe ser un valor positivo.")]
        public int PartidosGanados { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos empatados debe ser un valor positivo.")]
        public int PartidosEmpatados { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de partidos perdidos debe ser un valor positivo.")]
        public int PartidosPerdidos { get; set; }

        // Calculo de puntos (3 puntos por victoria, 1 por empate)
        public int Puntos
        {
            get
            {
                return (PartidosGanados * 3) + (PartidosEmpatados);
            }
        }
    }
}
