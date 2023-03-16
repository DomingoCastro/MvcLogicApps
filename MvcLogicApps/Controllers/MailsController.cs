using Microsoft.AspNetCore.Mvc;
using MvcLogicApps.Services;
using System.Net;
using System.Net.Mail;

namespace MvcLogicApps.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;
        private ServiceLogicApps service;

        public MailsController(IConfiguration configuration, ServiceLogicApps service)
        {
            this.configuration = configuration;
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string para, string asunto, string mensaje)
        {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            MailMessage email = new MailMessage();
            email.From = new MailAddress(user);
            email.To.Add(new MailAddress(para));
            email.Subject = asunto;
            email.Body = mensaje;
            //DEBEMOS CONFIGURAR EL OBJETO QUE SE ENCARGA DE ENVIAR EL MAIL
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Smtp:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port");
            bool enableSSL = this.configuration.GetValue<bool>("MailSettings:Smtp:EnableSSL");
            bool defaultcredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");
            //CREAMOS EL CLIENTE SMTP
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Host = host;
            smtpclient.Port = port;
            smtpclient.EnableSsl = enableSSL;
            smtpclient.UseDefaultCredentials= defaultcredentials;
            //CONFIGURAMOS LA SEGURIDAD DE LA CUENTA
            NetworkCredential credentials = new NetworkCredential(user, password);
            smtpclient.Credentials = credentials;
            smtpclient.Send(email);
            ViewData["MENSAJE"] = "Email enviado correctamente";
            return View();
        }
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string para, string asunto, string mensaje)
        {
            await this.service.SendMailAsync(para, asunto, mensaje);
            ViewData["MENSAJE"] = "Logic Apps Enviando mail correctamente.";
            return View();
        }
    }
}
