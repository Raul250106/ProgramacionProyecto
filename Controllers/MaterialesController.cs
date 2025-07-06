using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var materiales = LeerMateriales();
            return View(materiales);
        }

        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso restringido solo para docentes.";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Material material)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso restringido solo para docentes.";
                return RedirectToAction("Index", "Home");
            }
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
