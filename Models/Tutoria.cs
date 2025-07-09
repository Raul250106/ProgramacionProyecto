using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    // Modelo que representa una tutoria con sus propiedades básicas.
    public class Tutoria
    {
        // Identificador único que distingue cada registro en la tabla.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tema es obligatorio.")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "El nombre del tutor es obligatorio.")]
        public string Tutor { get; set; }

        public DateTime Fecha { get; set; }
    }
}
