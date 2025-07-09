using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tarea = _context.Tareas.ToList();
            return View(tarea);
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
        public IActionResult Crear(Tarea tarea)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Tareas.Add(tarea);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarea);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var Tarea = _context.Tareas.FirstOrDefault(t => t.Id == id);
            if (Tarea != null)
            {
                _context.Tareas.Remove(Tarea);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
