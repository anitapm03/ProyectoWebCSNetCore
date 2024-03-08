using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;

namespace ProyectoWebCSNetCore.Repositories
{
    public class RepositoryProvincias
    {
        private CSContext context;

        public RepositoryProvincias(CSContext context)
        {
            this.context = context;
        }

        public List<Provincia> GetProvincias()
        {
            var consulta = from datos in this.context.Provincias
                           select datos;
            return consulta.ToList();
        }
    }
}
