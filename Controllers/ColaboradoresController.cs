using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ProyectoFinalPA.Models;
using System.Collections.Generic;

namespace ProyectoFinalPA.Controllers
{
    public class ColaboradoresController : Controller
    {
        // Acción asincrónica que se ejecuta al abrir la página de inicio
        public async Task<IActionResult> Index()
        {
            // Creamos una lista vacía donde almacenaremos los colaboradores recibidos
            List<Colaborador> colaboradores1 = new List<Colaborador>();

            // Instanciamos un cliente HTTP para hacer solicitudes al servidor
            using (HttpClient client = new HttpClient())
            {
                // URL del servicio público
                string url = "https://jsonplaceholder.typicode.com/users";

                // Enviamos una solicitud HTTP GET a la URL indicada
                HttpResponseMessage response = await client.GetAsync(url);

                // Verificamos que la respuesta fue exitosa (HTTP 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Leemos el contenido de la respuesta como una cadena JSON
                    string json = await response.Content.ReadAsStringAsync();

                    // Convertimos (deserializamos) ese JSON en una lista de objetos Colaborador
                    colaboradores1 = JsonConvert.DeserializeObject<List<Colaborador>>(json);
                }
            }

            // Enviamos la lista de colaboradores a la vista para que sean mostrados
            return View(colaboradores1);
        }
    }
}
