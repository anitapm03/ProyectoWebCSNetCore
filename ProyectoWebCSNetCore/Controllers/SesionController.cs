using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;

namespace ProyectoWebCSNetCore.Controllers
{
    public class SesionController : Controller
    {
        private RepositorySesion repoSesion;

        public SesionController(RepositorySesion repoSesion)
        {
            this.repoSesion = repoSesion;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contraseña)
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(string nombre, string email,
            string contrasena, string bio)
        {
            this.repoSesion.InsertarUsuario(nombre, email, contrasena, bio);
            return View();
        }
    }
}
