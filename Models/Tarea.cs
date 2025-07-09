using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    // Modelo que representa una tarea con sus propiedades básicas.
    public class Tarea
    {
        // Identificador único que distingue cada registro en la tabla.
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaEntrega { get; set; }

        public string Materia { get; set; }
    }
}
