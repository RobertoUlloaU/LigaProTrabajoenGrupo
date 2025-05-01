using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LigaProTrabajoenGrupo.Models
{
    public class Jugador
    {
        [Key]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El nombre del jugador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del jugador no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Range(1, 99, ErrorMessage = "El número de camiseta debe estar entre 1 y 99.")]
        public int NumeroCamiseta { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de goles no puede ser negativo.")]
        public int Goles { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de asistencias no puede ser negativo.")]
        public int Asistencias { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El sueldo debe ser un valor positivo.")]
        public double Sueldo { get; set; }


        [ForeignKey("Equipo")]
        public int EquipoId { get; set; }

        // Relacion con el modelo Equipo
        public Equipo Equipo { get; set; }


    }

}
