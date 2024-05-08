using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string mensaje= string.Empty;

            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.nombres) ? "" : objeto.nombres;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.apellidos) ? "" : objeto.apellidos;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.correo) ? "" : objeto.correo;

            if(objeto.clave != objeto.ConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            resultado =  new CN_Cliente().Registrar(objeto, out mensaje);

            if(resultado > 0) {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
           
        }
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Cliente oCliente = null;

            oCliente = new CN_Cliente().Listar().Where(item => item.correo == correo &&  item.clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();
            
            if(oCliente == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if (oCliente.restablecer)
                {
                    TempData["idCliente"] = oCliente.idCliente;
                    return RedirectToAction("CambiarClave", "Acceso");
                }else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.correo, false);

                    Session["Cliente"] = oCliente;

                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }
            
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Cliente cliente = new Cliente();

            cliente = new CN_Cliente().Listar().Where(item => item.correo == correo).FirstOrDefault();

            if (cliente == null)
            {
                ViewBag.Error = "No se encontró un cliente relacionado a ese correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().ReestablecerClave(cliente.idCliente, correo, out mensaje);

            if (respuesta)
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

        [HttpPost]
        public ActionResult CambiarClave(string idcliente, string claveactual, string nuevaclave, string confirmarclave)
        {
            Cliente oCliente = new Cliente();

            oCliente = new CN_Cliente().Listar().Where(u => u.idCliente == int.Parse(idcliente)).FirstOrDefault();

            if (oCliente.clave != CN_Recursos.ConvertirSha256(claveactual))
            {


                TempData["IdCliente"] = oCliente.idCliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();

            }
            else if (nuevaclave != confirmarclave)
            {


                TempData["IdCliente"] = oCliente.idCliente;
                ViewData["vclave"] = "claveactual";
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";

            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);

            string mensaje = string.Empty;

            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(idcliente), nuevaclave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdCliente"] = oCliente.idCliente;
                ViewBag.Error = mensaje;
                return View();
            }

           
        }

        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}