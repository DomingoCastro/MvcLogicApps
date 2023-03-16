using Microsoft.AspNetCore.Mvc;
using MvcLogicApps.Models;
using MvcLogicApps.Services;

namespace MvcLogicApps.Controllers
{
    public class LogicAppsController : Controller
    {
        private ServiceLogicApps service;

        public LogicAppsController(ServiceLogicApps service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SumarNumerosFlow()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SumarNumerosFlow(int num1, int num2)
        {
           string respuesta = await this.service.SumarNumerosAsync(num1, num2);
            ViewData["MENSAJE"] = respuesta;
            return View();
        }

        public IActionResult Tabla()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Tabla(int num)
        {
            List<Tabla> datos = await this.service.TablaMultiplicarAsync(num);
            return View(datos);

        }

        [HttpGet]
        public async Task<IActionResult> Series()
        {
            List<Series> datos = await this.service.ListaSeriesAsync();
            return View(datos);
        }

        public IActionResult LoginLogicApps()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> LoginLogicApps(string username, string password)
        {
            string token = await this.service.GetTokenEmpleadoAsync(username, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
            }
            else
            {
                ViewData["TOKEN"] = token;
            }
            return View();
        }
    }
}
