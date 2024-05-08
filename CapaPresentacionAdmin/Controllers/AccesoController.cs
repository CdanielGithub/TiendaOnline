using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

using System.Web.Security;


namespace CapaPresentacionAdmin.Controllers
{
    
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.correo == correo && u.clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if(oUsuario.reestablecer)
                {

                    TempData["IdUsuario"] = oUsuario.idUsuario;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.correo, false);

                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }
           
           
        }
        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.idUsuario == int.Parse(idusuario)).FirstOrDefault();

            if (oUsuario.clave != CN_Recursos.ConvertirSha256(claveactual)) {


                TempData["IdUsuario"] = oUsuario.idUsuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();

            }
            else if(nuevaclave != confirmarclave)
            {


                TempData["IdUsuario"] = oUsuario.idUsuario;
                ViewData["vclave"] = "claveactual";
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";

            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);

            string mensaje = string.Empty;

            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);

            if(respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = oUsuario.idUsuario;
                ViewBag.Error = mensaje;
                return View();
            }

            
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario ousuario = new Usuario();

            ousuario = new CN_Usuarios().Listar().Where(item => item.correo == correo).FirstOrDefault();

            if(ousuario == null)
            {
                ViewBag.Error = "No se encontró un usuario relacionado a ese correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().ReestablecerClave(ousuario.idUsuario, correo, out mensaje);

            if(respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                    return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }

    }
}