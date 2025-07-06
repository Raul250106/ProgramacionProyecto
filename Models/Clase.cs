using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    public class Clase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        public string Materia { get; set; }

        public string Docente { get; set; }

        public int DuracionMinutos { get; set; }
    }
}
