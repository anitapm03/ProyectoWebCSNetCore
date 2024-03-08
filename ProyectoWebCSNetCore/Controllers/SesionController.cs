using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Helpers;
using ProyectoWebCSNetCore.Models;
using ProyectoWebCSNetCore.Repositories;
using System;

namespace ProyectoWebCSNetCore.Controllers
{
    public class SesionController : Controller
    {
        private RepositorySesion repoSesion;
        private HelperPathProvider helperPathProvider;
        private HelperMails helperMails;
        private IWebHostEnvironment hostEnvironment;

        public SesionController(RepositorySesion repoSesion, 
            HelperPathProvider helperPathProvider, HelperMails helperMails,
            IWebHostEnvironment hostEnvironment)
        {
            this.repoSesion = repoSesion;
            this.helperPathProvider = helperPathProvider;
            this.helperMails = helperMails;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string contrasena)
        {
            Usuario user = await this.repoSesion.LogInUserAsync(email, contrasena);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("USUARIO", user.Nombre);
                HttpContext.Session.SetString("ROL", user.Rol.ToString());
                HttpContext.Session.SetString("IDUSUARIO", user.IdUsuario.ToString());
                //TempData["USER"] = user;
                ViewData["MENSAJE"] = user.Nombre;

                //redirigir a su respectivo panel
                if (user.Rol == 1)
                {
                    return RedirectToAction("PanelAdmin", "Admin");
                }
                else
                {
                    return RedirectToAction("Conciertos", "Home");
                }
            }
        }

        public async Task<IActionResult> ActivateUser()
        {
            string token = TempData["TOKEN"] as string;
            await this.repoSesion.ActivateUserAsync(token);
            ViewData["MENSAJE"] = "Cuenta activada correctamente";
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(string nombre, string email,
            string contrasena, string bio)
        {
            
            string token =
                this.repoSesion.InsertarUsuario(nombre, email, contrasena, bio);

            string serverUrl = this.helperPathProvider.MapUrlServerPath();

            TempData["TOKEN"] = token;
            
            serverUrl = serverUrl + "/Sesion/ActivateUser/" + token;
            string mensaje = "<h3>Usuario registrado</h3>";
            mensaje += "<p>Debe activar su cuenta con nosotros pulsando el siguiente enlace</p>";
            mensaje += "<p>" + serverUrl + "</p>";
            mensaje += "<a href='" + serverUrl + "'>" + serverUrl + "</a>";
            mensaje += "<p>Muchas gracias</p>";
            await this.helperMails.SendMailAsync(email, "Registro Usuario", mensaje);
            ViewData["MENSAJE"] = "Usuario registrado correctamente. " +
                " Hemos enviado un mail para activar su cuenta";
            return View();
            
        }

        //edición de perfil
        public async Task<IActionResult> EditarPerfil()
        {
            string idusuario = HttpContext.Session.GetString("IDUSUARIO");
            Usuario user = await this.repoSesion.FindUserAsync(idusuario);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditarPerfil(string nombre, string email, string bio)
        {
            int id = int.Parse(HttpContext.Session.GetString("IDUSUARIO"));
            Usuario user = this.repoSesion.ActualizarInfoUsuario(id, nombre, email, bio);
            return View(user);
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("USUARIO");
            HttpContext.Session.Remove("ROL");
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> CambiarFotoPerfil()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarFotoPerfil
            (IFormFile foto)
        {
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            string fileName = foto.FileName;

            string path = Path.Combine(rootFolder, "images", "users" , fileName);

            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            int id = int.Parse(HttpContext.Session.GetString("IDUSUARIO"));
            this.repoSesion.UpdatePicture(id, fileName);

            ViewData["MENSAJE"] = "subido en " + path;
            return RedirectToAction("EditarPerfil");
        }

        public IActionResult CambiarContrasena()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasena(string contrasena)
        {

            int id = int.Parse(HttpContext.Session.GetString("IDUSUARIO"));
            this.repoSesion.UpdatePassw(id, contrasena);
            
            return RedirectToAction("EditarPerfil");
        }
    }
}
