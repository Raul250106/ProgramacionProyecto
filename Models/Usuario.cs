using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Rol { get; set; } // "Docente" o "Estudiante"

        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; } // Temporalmente sin hashing
    }
}
