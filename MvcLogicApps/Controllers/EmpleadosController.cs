using Microsoft.AspNetCore.Mvc;
using MvcLogicApps.Models;
using MvcLogicApps.Services;

namespace MvcLogicApps.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceApiEmpleados service;

        public EmpleadosController (ServiceApiEmpleados service)
        {
            this.service = service;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            string token = await this.service.GetTokenAsync(username, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario Logeado";
                HttpContext.Session.SetString("TOKEN", token);
                return View();
            }
        }

        public async Task<IActionResult> PerfilEmpleado() 
        {
            string token = HttpContext.Session.GetString("TOKEN");
            if(token == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Empleado empleado = await this.service.GetPerfilEmpleadoAsync(token);
                return View(empleado);
            }
        }


        public async Task<IActionResult> Compis()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            if (token == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                List<Empleado> empleados = await this.service.GetCompis(token);
                return View(empleados);
            }
        }

    }
}
