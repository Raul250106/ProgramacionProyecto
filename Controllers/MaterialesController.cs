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
            var material = _context.Materiales.ToList();
            return View(material);
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
