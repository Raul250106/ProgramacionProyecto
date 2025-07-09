using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    // Controlador para gestionar operaciones CRUD sobre tareas.
    public class TareasController : Controller
    {
        // Conecta programa con base de datos y tablas.
        private readonly ApplicationDbContext _context;
        // Constructor recibe contexto para poder usar base de datos.
        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Obtiene lista completa de tareas y los muestra en vista.
        public IActionResult Index()
        {
            var tarea = _context.Tareas.ToList();
            return View(tarea);
        }
        // Muestra formulario para crear tareas solo si usuario es docente.
        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // Recibe datos, valida, guarda nueva tarea en base de datos.
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
        // Elimina tareas seleccionado por id en la base de datos y actualiza lista vista.
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
