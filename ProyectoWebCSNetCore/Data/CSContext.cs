using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ProyectoWebCSNetCore.Data
{
    public class CSContext: DbContext
    {
        public CSContext(DbContextOptions<CSContext> options)
            : base(options)
        { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<PeticionEvento> Peticiones { get; set; }
        public DbSet<Peticion> ListaPeticiones { get; set; }

        public DbSet<Sala> Salas { get; set; }
    }
}
