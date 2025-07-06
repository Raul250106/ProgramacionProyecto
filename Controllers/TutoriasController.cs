using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    public class TutoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tutorias = LeerTutoriasDesdeArchivo();
            return View(tutorias);
        }

        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Tutoria tutoria)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                GuardarTutoriaEnArchivo(tutoria);
                return RedirectToAction("Index");
            }
            return View(tutoria);
        }

        private List<Tutoria> LeerTutoriasDesdeArchivo()
        {
            var lista = new List<Tutoria>();
            if (System.IO.File.Exists(rutaArchivo))
            {
                var lineas = System.IO.File.ReadAllLines(rutaArchivo);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    lista.Add(new Tutoria
                    {
                        Id = int.Parse(datos[0]),
                        Tema = datos[1],
                        Tutor = datos[2],
                        Fecha = DateTime.Parse(datos[3])
                    });
                }
            }
            return lista;
        }

        private void GuardarTutoriaEnArchivo(Tutoria tutoria)
        {
            var tutorias = LeerTutoriasDesdeArchivo();
            int nuevoId = tutorias.Any() ? tutorias.Max(t => t.Id) + 1 : 1;
            tutoria.Id = nuevoId;

            using (var sw = System.IO.File.AppendText(rutaArchivo))
            {
                sw.WriteLine($"{tutoria.Id},{tutoria.Tema},{tutoria.Tutor},{tutoria.Fecha:yyyy-MM-dd}");
            }
        }
    }
}
