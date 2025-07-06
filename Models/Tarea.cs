using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaEntrega { get; set; }

        public string Materia { get; set; }
    }
}
