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
        public async Task<IActionResult> Index()
        {
            // Lista para guardar colaboradores recibidos desde API externa.
            List<Colaborador> colaboradores1 = new List<Colaborador>();

            using (HttpClient client = new HttpClient())
            {
                // URL de la API desde donde se obtienen colaboradores.
                string url = "https://jsonplaceholder.typicode.com/users";
                // Envía petición HTTP y espera respuesta de forma asíncrona.
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Lee contenido JSON recibido en forma de texto.
                    string json = await response.Content.ReadAsStringAsync();
                    // Convierte JSON en lista de objetos colaborador manejables.
                    colaboradores1 = JsonConvert.DeserializeObject<List<Colaborador>>(json);
                }
            }
            // Devuelve la lista para mostrarla en la vista web.
            return View(colaboradores1);
        }
    }
}
