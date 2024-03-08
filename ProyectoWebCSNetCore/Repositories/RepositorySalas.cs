using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;

namespace ProyectoWebCSNetCore.Repositories
{
    public class RepositorySalas
    {
        private CSContext context;

        public RepositorySalas(CSContext context)
        {
            this.context = context;
        }

        public List<Sala> GetSalas()
        {
            var consulta = from datos in this.context.Salas
                           select datos;
            return consulta.ToList();
        }
    }
}
