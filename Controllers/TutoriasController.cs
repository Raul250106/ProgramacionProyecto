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
            var tutoria = _context.Tutorias.ToList();
            return View(tutoria);
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
                _context.Tutorias.Add(tutoria);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutoria);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var Tutoria = _context.Tutorias.FirstOrDefault(t => t.Id == id);
            if (Tutoria != null)
            {
                _context.Tutorias.Remove(Tutoria);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
