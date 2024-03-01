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

        public HomeController(ILogger<HomeController> logger,
            RepositoryConciertos repoConciertos)
        {
            _logger = logger;
            this.repoConciertos =  repoConciertos;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}