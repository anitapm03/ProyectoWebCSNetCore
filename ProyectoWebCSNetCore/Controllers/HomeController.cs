using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;
using System.Diagnostics;

namespace ProyectoWebCSNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RepositoryConciertos repoConciertos;
        private RepositoryArtistas repoArtistas;
        private RepositoryPublicaciones repoPublicaciones;

        public HomeController(ILogger<HomeController> logger,
            RepositoryConciertos repoConciertos,
            RepositoryArtistas repoArtistas,
            RepositoryPublicaciones repoPublicaciones)
        {
            _logger = logger;
            this.repoConciertos =  repoConciertos;
            this.repoArtistas = repoArtistas;
            this.repoPublicaciones = repoPublicaciones;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Quienes()
        {
            return View();
        }

        public IActionResult Conciertos()
        {
            List<Evento> eventos = this.repoConciertos.GetEventos();
            return View(eventos);
        }

        public IActionResult DetallesConcierto(int idevento)
        {
            Evento evento = this.repoConciertos.FindEvento(idevento);
            return View(evento);
        }

        public IActionResult Artistas()
        {
            List<Artista> artistas = this.repoArtistas.GetArtistas();
            return View(artistas);
        }

        public IActionResult DetallesArtista(int idartista)
        {
            Artista artista = this.repoArtistas.FindArtista(idartista);
            return View(artista);
        }

        public IActionResult Publicaciones()
        {
            List<UserPubli> publicaciones = this.repoPublicaciones.GetPublicaciones();
            return View(publicaciones);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}