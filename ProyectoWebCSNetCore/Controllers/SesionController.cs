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

        public SesionController(RepositorySesion repoSesion, 
            HelperPathProvider helperPathProvider, HelperMails helperMails)
        {
            this.repoSesion = repoSesion;
            this.helperPathProvider = helperPathProvider;
            this.helperMails = helperMails;
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
                ViewData["MENSAJE"] = user.Nombre;
                return View(user);
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

        
    }
}
