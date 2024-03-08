using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;

namespace ProyectoWebCSNetCore.Controllers
{
    public class AccionesController : Controller
    {
        private RepositoryProvincias repoProvincias;
        private RepositoryPeticiones repoPeticiones;

        public AccionesController(
            RepositoryProvincias repoProvincias,
            RepositoryPeticiones repoPeticiones)
        {
            this.repoProvincias = repoProvincias;
            this.repoPeticiones = repoPeticiones;
        }

        public IActionResult SolicitarConcierto()
        {
            List<Provincia> provincias = 
                this.repoProvincias.GetProvincias();
            return View(provincias);
        }

        [HttpPost]
        public IActionResult SolicitarConcierto
            (string nombre, int idprovincia, DateTime fecha, string telefono)
        {
            this.repoPeticiones.InsertarPeticion(nombre, idprovincia, fecha, telefono);
            return RedirectToAction("Conciertos", "Home");
        }

    }
}
