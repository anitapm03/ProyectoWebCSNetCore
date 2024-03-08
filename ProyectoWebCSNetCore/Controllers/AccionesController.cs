using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;

namespace ProyectoWebCSNetCore.Controllers
{
    public class AccionesController : Controller
    {
        private RepositoryProvincias repoProvincias;

        public AccionesController(RepositoryProvincias repoProvincias)
        {
            this.repoProvincias = repoProvincias;
        }

        public IActionResult SolicitarConcierto()
        {
            List<Provincia> provincias = 
                this.repoProvincias.GetProvincias();
            return View(provincias);
        }

        [HttpPost]
        public IActionResult SolicitarConcierto
            ()
        {
            return View();
        }
    }
}
