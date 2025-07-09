using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ProyectoFinalPA.Models;

namespace ProyectoFinalPA.Controllers
{
    public class UsuariosController : Controller
    {
        // Contexto conecta programa con base datos y permite consultas.
        private readonly ApplicationDbContext _context;
        // Constructor recibe contexto para poder usar base de datos.
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Muestra formulario para que usuario ingrese sus credenciales.
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Busca usuario con correo y contraseña que coincidan.
                var usuario = _context.Usuarios
                    .FirstOrDefault(u => u.Correo == model.Correo && u.Contrasena == model.Contrasena);

                if (usuario != null)
                {
                    // Guarda datos usuario en sesión para controlar acceso.
                    HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                    HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

                    return RedirectToAction("Index", "Home");
                }
                // Muestra error si usuario o contraseña no coinciden.
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
            }
            // Valida y procesa el inicio de sesión de usuario.
            return View(model);
        }
        // Muestra formulario para que nuevo usuario se registre.
        public IActionResult Registrar()
        {
            return View();
        }
        // Guarda nuevo usuario en base datos tras validar información.
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
        // Limpia sesión actual y redirige a página de login.
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
