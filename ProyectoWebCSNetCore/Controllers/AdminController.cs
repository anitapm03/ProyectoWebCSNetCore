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
        private RepositoryProvincias repoProvincias;
        private IWebHostEnvironment hostEnvironment;
        private RepositoryArtistas repoArtistas;
        private RepositoryGeneros repoGeneros;
        private RepositorySesion repoSesion;
        private RepositoryPublicaciones repoPublicaciones;

        public AdminController(RepositoryPeticiones repoPeticiones, 
            RepositoryConciertos repoConciertos, 
            RepositorySalas repoSalas,
            RepositoryProvincias repoProvincias,
            IWebHostEnvironment hostEnvironment,
            RepositoryArtistas repoArtistas,
            RepositoryGeneros repoGeneros,
            RepositorySesion repoSesion,
            RepositoryPublicaciones repoPublicaciones)
        {
            this.repoPeticiones = repoPeticiones;
            this.repoConciertos = repoConciertos;
            this.repoSalas = repoSalas;
            this.repoProvincias = repoProvincias;
            this.hostEnvironment = hostEnvironment;
            this.repoArtistas = repoArtistas;
            this.repoGeneros = repoGeneros;
            this.repoSesion = repoSesion;
            this.repoPublicaciones = repoPublicaciones;
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

        public IActionResult EliminarPeticion(int id)
        {
            this.repoPeticiones.EliminarPeticion(id);
            return RedirectToAction("VerPeticiones");
        }

        public IActionResult VerConciertos()
        {
            List<Evento> conciertos = this.repoConciertos.GetEventos(); 
            return View(conciertos);
        }

        public IActionResult EliminarConcierto(int id)
        {
            this.repoConciertos.EliminarConcierto(id);
            return RedirectToAction("VerConciertos");
        }

        public async Task<IActionResult> CrearConciertoAsync()
        {
            List<Sala> salas = this.repoSalas.GetSalas();
            return View(salas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearConciertoAsync
            (string nombre, DateTime fecha, IFormFile foto,
            string entradas, int sala, string grupo)
        {
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            string fileName = foto.FileName;

            string path = Path.Combine(rootFolder, "images", "eventos", fileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            this.repoConciertos.InsertarConcierto
                (nombre, fecha, fileName, entradas, sala, grupo);
            return RedirectToAction("VerConciertos");
        }

        public IActionResult VerSalas()
        {
            List<Sala> salas = this.repoSalas.GetSalas();
            return View(salas);
        }

        public IActionResult EliminarSala(int id)
        {
            this.repoSalas.EliminarSala(id);
            return RedirectToAction("VerSalas");
        }

        public IActionResult CrearSala()
        {
            List<Provincia> provincias = this.repoProvincias.GetProvincias();
            return View(provincias);
        }
        [HttpPost]
        public IActionResult CrearSala(string direccion, string nombre, int provincia)
        {
            this.repoSalas.CrearSala(direccion, nombre, provincia);
            return RedirectToAction("VerSalas");
        }

        public IActionResult VerArtistas()
        {
            List<Artista> artistas = this.repoArtistas.GetArtistas(); 
            return View(artistas);
        }

        public IActionResult EliminarArtista(int id)
        {
            this.repoArtistas.EliminarArtista(id);
            return RedirectToAction("VerArtistas");
        }
        public async Task<IActionResult> CrearArtista()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearArtista
            (string nombre, IFormFile foto, string spotify, string descripcion)
        {
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            string fileName = foto.FileName;

            string path = Path.Combine(rootFolder, "images", "artistas", fileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            this.repoArtistas.InsertarArtista(nombre, fileName, spotify, descripcion);

            return RedirectToAction("VerArtistas");
        }

        public IActionResult VerGeneros()
        {
            List<Genero> generos = this.repoGeneros.GetGeneros();
            return View(generos);
        }
        public IActionResult EliminarGenero(int id)
        {
            this.repoGeneros.EliminarGenero(id);
            return RedirectToAction("VerGeneros");
        }
        public IActionResult CrearGenero()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearGenero(string nombre)
        {
            this.repoGeneros.InsertGenero(nombre);
            return RedirectToAction("VerGeneros");
        }

        public IActionResult VerUsuarios()
        {
            List<Usuario> users = this.repoSesion.GetUsuarios();
            return View(users);
        }

        public IActionResult EliminarUsuario(int id)
        {
            this.repoSesion.EliminarUsuario(id);
            return RedirectToAction("VerUsuarios");
        }

        public IActionResult VerPublicaciones()
        {
            List<UserPubli> userPublis = this.repoPublicaciones.GetPublicaciones();
            return View(userPublis);
        }

        public IActionResult EliminarPubli(int id)
        {
            this.repoPublicaciones.EliminarPubli(id);
            return RedirectToAction("VerPublicaciones");
        }
    }
}
