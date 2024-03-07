using Microsoft.AspNetCore.Mvc;

namespace ProyectoWebCSNetCore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult PanelAdmin()
        {
            return View();
        }
    }
}
