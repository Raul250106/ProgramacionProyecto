using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    public class ClasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clases = LeerClases();
            return View(clases);
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
        public IActionResult Crear(Clase clase)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso restringido solo para docentes.";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                GuardarClase(clase);
                return RedirectToAction("Index");
            }
            return View(clase);
        }

        private List<Clase> LeerClases()
        {
            var lista = new List<Clase>();
            if (System.IO.File.Exists(rutaArchivo))
            {
                var lineas = System.IO.File.ReadAllLines(rutaArchivo);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    lista.Add(new Clase
                    {
                        Id = int.Parse(datos[0]),
                        Materia = datos[1],
                        Docente = datos[2],
                        DuracionMinutos = int.Parse(datos[3])
                    });
                }
            }
            return lista;
        }

        private void GuardarClase(Clase clase)
        {
            var clases = LeerClases();
            int nuevoId = clases.Any() ? clases.Max(c => c.Id) + 1 : 1;
            clase.Id = nuevoId;

            using (var sw = System.IO.File.AppendText(rutaArchivo))
            {
                sw.WriteLine($"{clase.Id},{clase.Materia},{clase.Docente},{clase.DuracionMinutos}");
            }
        }
    }
}