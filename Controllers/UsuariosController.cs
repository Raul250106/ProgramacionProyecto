using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ProyectoFinalPA.Models;

namespace ProyectoFinalPA.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios
                    .FirstOrDefault(u => u.Correo == model.Correo && u.Contrasena == model.Contrasena);

                if (usuario != null)
                {
                    // Guardar datos en sesión
                    HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                    HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
            }

            return View(model);
        }

        // GET: Usuarios/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Usuarios/Registrar
        [HttpPost]
        public IActionResult Registrar(Usuario nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(nuevoUsuario);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
