using Microsoft.AspNetCore.Mvc;
using ProyectoFinalPA.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProyectoFinalPA.Controllers
{
    // Controlador para gestionar operaciones CRUD sobre materiales.
    public class MaterialesController : Controller
    {
        // Conecta programa con base de datos y tablas.
        private readonly ApplicationDbContext _context;
        // Constructor recibe contexto para poder usar base de datos.
        public MaterialesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Obtiene lista completa de materiales y los muestra en vista.
        public IActionResult Index()
        {
            var material = _context.Materiales.ToList();
            return View(material);
        }
        // Muestra formulario para crear material solo si usuario es docente.
        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // Recibe datos, valida, guarda nuevo material en base de datos.
        [HttpPost]
        public IActionResult Crear(Material material)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Docente")
            {
                TempData["Error"] = "Acceso negado";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Materiales.Add(material);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(material);
        }
        // Elimina material seleccionado por id en la base de datos y actualiza lista vista.
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var Material = _context.Materiales.FirstOrDefault(t => t.Id == id);
            if (Material != null)
            {
                _context.Materiales.Remove(Material);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
