using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // son el parametro el tipo de archivo de models y el nombre es el mismo que 
        // el de la base de dato
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Curso> Curso { get; set; }

        // Se lo parcea porque no estan con el nombre del tipo Id si o Id_Algo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>()
                .HasKey(c => c.Id_Curso);

            modelBuilder.Entity<Estudiante>()
                .HasKey(e => e.Id_Estudiante);
        }
    }
}
