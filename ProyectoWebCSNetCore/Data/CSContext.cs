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
    }
}
