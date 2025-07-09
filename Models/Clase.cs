using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    // Modelo que representa una clase con sus propiedades básicas.
    public class Clase
    {
        // Identificador único que distingue cada registro en la tabla.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        public string Materia { get; set; }

        public string Docente { get; set; }

        public int DuracionMinutos { get; set; }
    }
}
