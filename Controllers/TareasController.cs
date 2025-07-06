using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    public class TareasController : Controller
    {
        private readonly string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "AppData", "tareas.txt");

        public IActionResult Index()
        {
            var tareas = LeerTareas();
            return View(tareas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                GuardarTarea(tarea);
                return RedirectToAction("Index");
            }
            return View(tarea);
        }

        private List<Tarea> LeerTareas()
        {
            var lista = new List<Tarea>();
            if (System.IO.File.Exists(rutaArchivo))
            {
                var lineas = System.IO.File.ReadAllLines(rutaArchivo);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    lista.Add(new Tarea
                    {
                        Id = int.Parse(datos[0]),
                        Descripcion = datos[1],
                        Materia = datos[2],
                        FechaEntrega = DateTime.Parse(datos[3])
                    });
                }
            }
            return lista;
        }

        private void GuardarTarea(Tarea tarea)
        {
            var tareas = LeerTareas();
            int nuevoId = tareas.Any() ? tareas.Max(x => x.Id) + 1 : 1;
            tarea.Id = nuevoId;

            using (var sw = System.IO.File.AppendText(rutaArchivo))
            {
                sw.WriteLine($"{tarea.Id},{tarea.Descripcion},{tarea.Materia},{tarea.FechaEntrega:yyyy-MM-dd}");
            }
        }
    }
}
