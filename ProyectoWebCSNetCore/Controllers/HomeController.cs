using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache memoryCache;

        public HomeController(ILogger<HomeController> logger,
            RepositoryConciertos repoConciertos,
            RepositoryArtistas repoArtistas,
            RepositoryPublicaciones repoPublicaciones,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            this.repoConciertos =  repoConciertos;
            this.repoArtistas = repoArtistas;
            this.repoPublicaciones = repoPublicaciones;
            this.memoryCache = memoryCache;
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
            if(this.memoryCache.Get("FAV") != null)
            {
                List<Evento> favoritos =
                    this.memoryCache.Get<List<Evento>>("FAV");
                ViewData["FAVORITOS"] = favoritos;
            }

            List<Evento> eventos = this.repoConciertos.GetEventos();
            return View(eventos);
        }


        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Client)]
        public IActionResult DetallesConcierto(int idevento, int? idfav)
        {
            

            if (idfav != null)
            {
                List<Evento> eventosFav;
                if(this.memoryCache.Get("FAV") == null)
                {
                    eventosFav = new List<Evento>();
                }
                else
                {
                    eventosFav = this.memoryCache.Get<List<Evento>>("FAV");
                }
                Evento evento = this.repoConciertos.FindEvento(idfav.Value);
                eventosFav.Add(evento);

                this.memoryCache.Set("FAV", eventosFav);
                return View(evento);
            }
            else
            {
                Evento evento = this.repoConciertos.FindEvento(idevento);
                return View(evento);
            }

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