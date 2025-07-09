using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalPA.Models
{
    // Modelo que representa un material con sus propiedades básicas.
    public class Material
    {
        // Identificador único que distingue cada registro en la tabla.
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Solicitante { get; set; }

        public DateTime FechaReserva { get; set; }
    }
}
