using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalPA.Models
{
    // Clase principal que gestiona la conexión con la base de datos.
    public class ApplicationDbContext : DbContext
    {
        // Constructor que inicializa la base de datos con opciones configuradas externamente.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define tablas de la base de datos para cada modelo usado.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tutoria> Tutorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<Material> Materiales { get; set; }
    }
}
