using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    // Controlador para gestionar operaciones CRUD sobre tutorias.
    public class TutoriasController : Controller
    {
        // Conecta programa con base de datos y tablas.
        private readonly ApplicationDbContext _context;
        // Constructor recibe contexto para poder usar base de datos.
        public TutoriasController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Obtiene lista completa de tutorias y los muestra en vista.
        public IActionResult Index()
        {
            var tutoria = _context.Tutorias.ToList();
            return View(tutoria);
        }
        // Muestra formulario para crear tutorias solo si usuario es docente.
        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // Recibe datos, valida, guarda nueva tutoria en base de datos.
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
        // Elimina tutorias seleccionado por id en la base de datos y actualiza lista vista.
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
