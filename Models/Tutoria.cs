using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    public class Tutoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tema es obligatorio.")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "El nombre del tutor es obligatorio.")]
        public string Tutor { get; set; }

        public DateTime Fecha { get; set; }
    }
}
