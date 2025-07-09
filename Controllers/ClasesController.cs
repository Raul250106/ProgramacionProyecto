using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    // Controlador para gestionar las operaciones CRUD de clases.
    public class ClasesController : Controller
    {
        // Conecta programa con base de datos y sus tablas.
        private readonly ApplicationDbContext _context;

        public ClasesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Obtiene lista de clases desde base y la muestra.
        public IActionResult Index()
        {
            var Clase = _context.Clases.ToList();
            return View(Clase);
        }
        // Muestra formulario para crear clase solo si es docente.
        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // Recibe datos nuevos, valida y guarda clase en base datos.
        [HttpPost]
        public IActionResult Crear(Clase clase)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                _context.Clases.Add(clase);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clase);
        }
        // Borra clase por id en la base de datos y actualiza la vista de clases.
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var Clase = _context.Clases.FirstOrDefault(t => t.Id == id);
            if (Clase != null)
            {
                _context.Clases.Remove(Clase);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}