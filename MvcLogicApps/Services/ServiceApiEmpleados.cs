using MvcLogicApps.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace MvcLogicApps.Services
{
    public class ServiceApiEmpleados
    {

        private string urlApi;

        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiEmpleados(IConfiguration configuration)
        {
            this.urlApi = configuration.GetValue<string>("ApiUrls:ApiAuthEmpleados");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress= new Uri(this.urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var model = new
                {
                    username= username,
                    password= password
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/auth/login";
                HttpResponseMessage response = await client.PostAsync(request, content);
                if(response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    //CONVERTIMOS EL DATA EN UN OBJETO DATA OBJECT PARA OBTENER EL RESPONSE
                    JObject Jobject = JObject.Parse(data);
                    //MANUALMENTE ACCEDEMOS A LA CLAVE repsonse
                    string token = Jobject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<Empleado> GetPerfilEmpleadoAsync(string token) 
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/empleados/perfilusuario";
                client.BaseAddress= new Uri(this.urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //AÑADIMOS LA AUTORIZACIÓN. DEBEMOS RECORDAR QUE TENEMOS QUE ENVIAR TAMBIEN bearer.
                //DENTRO DEL HEADER Authorization
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
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

        public async Task<List<Empleado>> GetCompis(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/empleados/compis";
                client.BaseAddress = new Uri(this.urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
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
