using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "AppData", "materiales.txt");

        public IActionResult Index()
        {
            var materiales = LeerMateriales();
            return View(materiales);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Material material)
        {
            if (ModelState.IsValid)
            {
                GuardarMaterial(material);
                return RedirectToAction("Index");
            }
            return View(material);
        }

        private List<Material> LeerMateriales()
        {
            var lista = new List<Material>();
            if (System.IO.File.Exists(rutaArchivo))
            {
                var lineas = System.IO.File.ReadAllLines(rutaArchivo);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    lista.Add(new Material
                    {
                        Id = int.Parse(datos[0]),
                        Nombre = datos[1],
                        Solicitante = datos[2],
                        FechaReserva = DateTime.Parse(datos[3])
                    });
                }
            }
            return lista;
        }

        private void GuardarMaterial(Material material)
        {
            var materiales = LeerMateriales();
            int nuevoId = materiales.Any() ? materiales.Max(x => x.Id) + 1 : 1;
            material.Id = nuevoId;

            using (var sw = System.IO.File.AppendText(rutaArchivo))
            {
                sw.WriteLine($"{material.Id},{material.Nombre},{material.Solicitante},{material.FechaReserva:yyyy-MM-dd}");
            }
        }
    }
}
