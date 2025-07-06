using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Rol { get; set; } // "Docente" o "Estudiante"

        [Required]
        public string Contrasena { get; set; } // Temporalmente sin hashing
    }
}
