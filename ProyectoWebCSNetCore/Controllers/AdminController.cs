using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;

namespace ProyectoWebCSNetCore.Controllers
{
    public class AdminController : Controller
    {
        private RepositoryPeticiones repoPeticiones;
        private RepositoryConciertos repoConciertos;
        private RepositorySalas repoSalas;

        public AdminController(RepositoryPeticiones repoPeticiones, 
            RepositoryConciertos repoConciertos, 
            RepositorySalas repoSalas)
        {
            this.repoPeticiones = repoPeticiones;
            this.repoConciertos = repoConciertos;
            this.repoSalas = repoSalas;
        }

        public IActionResult PanelAdmin()
        {
            return View();
        }

        public IActionResult VerPeticiones()
        {
            List<Peticion> peticiones = this.repoPeticiones.GetListaPeticiones();
            return View(peticiones);
        }

        public IActionResult VerConciertos()
        {
            List<Evento> conciertos = this.repoConciertos.GetEventos(); 
            return View(conciertos);
        }

        public IActionResult CrearConcierto()
        {
            List<Sala> salas = this.repoSalas.GetSalas();
            return View(salas);
        }

        [HttpPatch]
        public IActionResult CrearConcierto(Concierto concierto)
        {
            return RedirectToAction("VerConciertos");
        }
    }
}
