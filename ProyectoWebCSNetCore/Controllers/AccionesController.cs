using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;

namespace ProyectoWebCSNetCore.Controllers
{
    public class AccionesController : Controller
    {
        private RepositoryProvincias repoProvincias;
        private RepositoryPeticiones repoPeticiones;
        private RepositoryPublicaciones repoPublicaciones;
        private IWebHostEnvironment hostEnvironment;

        public AccionesController(
            RepositoryProvincias repoProvincias,
            RepositoryPeticiones repoPeticiones,
            RepositoryPublicaciones repoPublicaciones,
            IWebHostEnvironment hostEnvironment)
        {
            this.repoProvincias = repoProvincias;
            this.repoPeticiones = repoPeticiones;
            this.repoPublicaciones = repoPublicaciones;
            this.hostEnvironment = hostEnvironment;
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

        public IActionResult CrearPubli()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPubli
            (string texto, IFormFile foto)
        {
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            string fileName = foto.FileName;

            string path = Path.Combine(rootFolder, "images", "publicaciones", fileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            int idusuario = int.Parse(HttpContext.Session.GetString("IDUSUARIO"));
            DateTime fecha = DateTime.Now;

            this.repoPublicaciones.InsertPubli(idusuario, texto, fileName, fecha);
            return RedirectToAction("Publicaciones", "Home");
        }


    }
}
