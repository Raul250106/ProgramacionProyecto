using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Solicitante { get; set; }

        public DateTime FechaReserva { get; set; }
    }
}
