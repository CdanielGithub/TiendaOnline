using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var resultado = ListarUsuarios();
            return View(resultado);
        }


        public ActionResult Usuarios()
        {
            return View();
        }

        public List<Usuario> ListarUsuarios()
        {
            
            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();

            // return Json(oLista,JsonRequestBehavior.AllowGet);
            return oLista;
        }

    }
}