using MvcLogicApps.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcLogicApps.Services
{
    public class ServiceLogicApps
    {
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceLogicApps() 
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //LAS LLAMADAS A URL SIEMPRE SON ASINCRONAS

        public async Task SendMailAsync(string email, string subject, string body)
        {
            //NECESITAMOS LA URL DE PETICION A NUESTRO FLOW DE MAIL
            string urlMail = "https://prod-13.westeurope.logic.azure.com:443/workflows/e56d1e8a0a8b4193aa2c81f00a382c64/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=xMtOee4F3GGJq72b2s7gi4PQsSAkyTCPpGDdwYAvRm8";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //CREAMOS NUESTRO MODEL CON LOS DATOS RECIBIDOS EN EL METODO
                EmailModel model = new EmailModel
                {
                    Email = email,
                    Asunto = subject,
                    Cuerpo = body
                };
                //CONVERTIMOS A NUESTRO MODELO A JSON
                string json = JsonConvert.SerializeObject(model);
                //ENVIAMOS LOS DATOS A NUESTRO FLOW
                //PARA ELLO ENVIAMOS LOS DATOS UTILIZANDO StringContent
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                //POR ULTIMO HACEMOS LA PETICION POST
                HttpResponseMessage response = await client.PostAsync(urlMail, content);
                //AQUI COMPROBARIAMOS LA RESPUESTA DE NUESTRA PETICION PERO ESTAMOS DEVOLVIENDO SIEMPRE 200
                //NO HAREMOS NADA
            }
        }
        public async Task<string> SumarNumerosAsync(int num1, int num2)
        {
            string urlFlow = "https://prod-89.westeurope.logic.azure.com:443/workflows/71d2ec12f37f4218b365fa2b4164e683/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Tx2tKsqW5xTFbjE632eXpnAIiMMvp4vY4C6phZ-vy1s";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //PARA ENVIAR LOS DATOS UTILIZAMOS UN OBJETO ANONIMO
                //NO SERA NECESARIO UN MODEL
                var model = new { numero1= num1, numero2= num2 };
                var json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlow, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return "La suma es: " + data;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Tabla>> TablaMultiplicarAsync(int num)
        {
            string urlFlow = "https://prod-245.westeurope.logic.azure.com:443/workflows/ecd06198586641e98f220b37f355cd1b/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Ts47-_-YNWRSf7OjhQPRhFVPq0Yx3WVO-3FWbCw3vpM";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var model = new { numero= num };
                var json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlow, content);
                if (response.IsSuccessStatusCode)
                {
                    List<Tabla> datos = await response.Content.ReadAsAsync<List<Tabla>>();
                    return datos;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Series>> ListaSeriesAsync()
        {
            string urlFlow = "https://prod-77.westeurope.logic.azure.com:443/workflows/76d94722cba64b9ea657ca8a5f272ca6/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=zLlBdLNUNPplPOjmRa8NvUSDXdGhV__y8nol8ncX3E8";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.PostAsync(urlFlow, null);
                if (response.IsSuccessStatusCode)
                {
                    List<Series> datos = await response.Content.ReadAsAsync<List<Series>>();
                    return datos;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<string> GetTokenEmpleadoAsync(string username, string password)
        {
            string urlFlow = "https://prod-140.westeurope.logic.azure.com:443/workflows/dd5464ca9bb2409b922a2bb3bf006de7/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=rJImvmY9oshW6mj7cMizLCBfNeoNse19wiSh8mJwLUo";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var model = new { username = username, password = password};
                var json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlow, content);
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<Empleado> GetEmpleadoAsycn(string token)
        {
            string urlFlow = "https://prod-145.westeurope.logic.azure.com:443/workflows/fa2638d70feb487ab04401554c0a34d4/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=_pt4zu-Ns2HO1PGj3t-rYbb5-boJKQBZSFADGchCnuA";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var model = new { token = token };
                var json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlow, content);
                if (response.IsSuccessStatusCode)
                {
                    Empleado empleado = await response.Content.ReadAsAsync<Empleado>();
                    return empleado;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Empleado>> GetCompisAsync(string token)
        {
            string urlFlow = "https://prod-73.westeurope.logic.azure.com:443/workflows/9637bbe9b5fd4a06b491374f68136fe2/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=gL13R1m-lvvLi8xCcHGuJvMfms1h9WObMHBnanHBrTg";
            using (HttpClient client = new HttpClient())
            {
                var model = new { token = token };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlow, content);
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> compis = await response.Content.ReadAsAsync<List<Empleado>>();
                    return compis;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
