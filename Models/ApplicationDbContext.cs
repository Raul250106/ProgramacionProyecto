using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalPA.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tutoria> Tutorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<Material> Materiales { get; set; }
    }
}
