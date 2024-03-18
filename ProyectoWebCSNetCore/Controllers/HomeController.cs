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
        private RepositoryRelaciones repoRela;
        private RepositoryGeneros repoGeneros;

        public HomeController(ILogger<HomeController> logger,
            RepositoryConciertos repoConciertos,
            RepositoryArtistas repoArtistas,
            RepositoryPublicaciones repoPublicaciones,
            IMemoryCache memoryCache,
            RepositoryRelaciones repoRela,
            RepositoryGeneros repoGeneros)
        {
            _logger = logger;
            this.repoConciertos =  repoConciertos;
            this.repoArtistas = repoArtistas;
            this.repoPublicaciones = repoPublicaciones;
            this.memoryCache = memoryCache;
            this.repoRela = repoRela;
            this.repoGeneros = repoGeneros;
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
            List<Evento> destacados = this.repoConciertos.GetDestacados();
            ViewData["DESTACADOS"] = destacados;

            List<Evento> eventos = this.repoConciertos.GetEventos();
            return View(eventos);
        }

        public IActionResult EliminarFavoritos(int idconcierto)
        {
            List<Evento> eventosFav;

            eventosFav = this.memoryCache.Get<List<Evento>>("FAV");
            
            int index = eventosFav.FindIndex(e => e.IdConcierto == idconcierto);
            if (index != -1)
            {
                eventosFav.RemoveAt(index);

                if (eventosFav.Count == 0)
                {
                    this.memoryCache.Remove("FAV");
                }
                else
                {
                    this.memoryCache.Set("FAV", eventosFav);
                }
            }
            return RedirectToAction("Conciertos");
        }

        
        public IActionResult DetallesConcierto(int idevento, int? idfav)
        {
            if(this.memoryCache.Get<List<Evento>>("FAV") != null)
            {
                List<Evento> listaFav = this.memoryCache.Get<List<Evento>>("FAV");

                int index = listaFav.FindIndex(e => e.IdConcierto == idevento);
                if (index != -1)
                {
                    ViewData["ESFAV"] = true;
                }
            }
            

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

                List<ArtistaConcierto> relaciones = this.repoRela.GetArtistasConcierto(idfav.Value);
                List<Artista> artistasConcierto = new List<Artista>();
                foreach (ArtistaConcierto artista in relaciones)
                {
                    Artista a = this.repoArtistas.FindArtista(artista.IdArtista);
                    artistasConcierto.Add(a);
                }

                ViewData["ARTISTASCONCIERTO"] = artistasConcierto;
                ViewData["ESFAV"] = true;

                return View(evento);
            }
            else
            {
                List<ArtistaConcierto> relaciones = this.repoRela.GetArtistasConcierto(idevento);
                List<Artista> artistasConcierto = new List<Artista>();
                foreach (ArtistaConcierto artista in relaciones)
                {
                    Artista a = this.repoArtistas.FindArtista(artista.IdArtista);
                    artistasConcierto.Add(a);
                }

                ViewData["ARTISTASCONCIERTO"] = artistasConcierto;

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

            List<ArtistaGenero> relaciones = this.repoRela.GetGenerosArtista(idartista);
            List<Genero> generosArtista = new List<Genero>();
            foreach (ArtistaGenero artistaGenero in relaciones)
            {
                Genero genero = this.repoGeneros.FindGenero(artistaGenero.IdGenero);
                generosArtista.Add(genero);
            }

            ViewData["GENEROSARTISTA"] = generosArtista;

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