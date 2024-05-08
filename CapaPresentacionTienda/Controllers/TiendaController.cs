using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.IO;
using System.Web.Services.Description;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using CapaPresentacionTienda.Filter;
using Stripe.Checkout;
using Stripe;



namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Producto().Listar().Where(p => p.idProducto == idproducto).FirstOrDefault();

            if(oProducto != null)
            {
                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaImagen, oProducto.nombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.nombreImagen);
            }
            return View(oProducto);
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();
            return Json(new{ data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMarcaporCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            lista = new CN_Marca().ListarMarcaporCategoria(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProducto(int idcategoria, int idmarca)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                idProducto = p.idProducto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                precio = p.precio,
                stock = p.stock,
                rutaImagen = p.rutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.rutaImagen, p.nombreImagen), out conversion),
                Extension = Path.GetExtension(p.nombreImagen),
                activo = p.activo
            }).Where(p =>
                p.oCategoria.idCategoria == (idcategoria == 0 ? p.oCategoria.idCategoria : idcategoria) && 
                p.oMarca.idMarca == (idmarca == 0 ? p.oMarca.idMarca : idmarca) &&
                p.stock > 0 && p.activo == true
            ).ToList();

            var jsonresult = Json(new {data = lista}, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int .MaxValue;

            return jsonresult;
        }

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;

            bool existe = new CN_Carrito().ExisteCarrito(idcliente, idproducto);

            bool respuesta = false;
            string mensaje = string.Empty;

            if(existe)
            {
                mensaje = "El producto ya existe en el carrito";
            }
            else
            {
                respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }
            return Json(new {respuesta = respuesta, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idcliente);

            return Json(new {cantidad = cantidad}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LitarProductosCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;

            List<Carrito> oLista = new List<Carrito>();

            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                oProducto = new Producto()
                {
                    idProducto = oc.oProducto.idProducto,
                    nombre = oc.oProducto.nombre,
                    oMarca = oc.oProducto.oMarca,
                    precio = oc.oProducto.precio,
                    rutaImagen = oc.oProducto.rutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.rutaImagen, oc.oProducto.nombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.nombreImagen)
                },
                cantidad = oc.cantidad
            }).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;

            bool respuesta = false;
            string mensaje = string.Empty;

            //respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);

            //respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true(sumar), out mensaje);
            //respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, sumar, out mensaje);
            respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, sumar, out mensaje);



            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;

            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcliente, idproducto);


            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerCiudad()
        {
            List<Ciudad> oLista = new List<Ciudad>();

            oLista = new CN_Ubicacion().ObtenerCiudad();
                              
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
          public JsonResult ObtenerSector()
          {

              List<Sector> oLista = new CN_Ubicacion().ObtenerSector();
              return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
          }


        [ValidarSession]
        [Authorize]
        public ActionResult Carrito()
        {
            return View();
        }

        [HttpPost]

      

         public async Task<JsonResult> ProcesarPago(List<Carrito> oListaCarrito, Venta oVenta)
         {
             decimal total = 0;

             DataTable detalle_venta = new DataTable();

             detalle_venta.Locale = new CultureInfo("es-RD");
             detalle_venta.Columns.Add("IdProducto", typeof(string));
             detalle_venta.Columns.Add("Cantidad", typeof(int));
             detalle_venta.Columns.Add("Total", typeof(decimal));

            // Esto es solo para las prubas INICIO

            List<Producto> oListaProducto = new List<Producto>();

           /* foreach (Carrito oCarrito in oListaCarrito)
            {
                decimal subtotal = Convert.ToDecimal(oCarrito.cantidad.ToString()) * oCarrito.oProducto.precio;
                total += subtotal;


                Producto nuevoProducto = new Producto
                {
                    idProducto = oCarrito.oProducto.idProducto,
                    nombre = oCarrito.oProducto.nombre,
                    descripcion = oCarrito.oProducto.descripcion,
                    precio = oCarrito.oProducto.precio,
                   
                    
                };

                
                oListaProducto.Add(nuevoProducto);

                
                detalle_venta.Rows.Add(new object[]
                {
                    oCarrito.oProducto.idProducto,
                    oCarrito.cantidad,
                    subtotal
                });
            }

                   string return_url= "https://localhost:44394/"*/





            //FIN

              foreach (Carrito oCarrito in oListaCarrito)
              {
                  decimal subtotal = Convert.ToDecimal(oCarrito.cantidad.ToString()) * oCarrito.oProducto.precio;
                  total += subtotal;


                  detalle_venta.Rows.Add(new object[]
                  {
              oCarrito.oProducto.idProducto,
              oCarrito.cantidad,
              subtotal
                  });
              }


              oVenta.montoTotal = total;
              oVenta.idCliente = ((Cliente)Session["Cliente"]).idCliente;
              oVenta.TotalProducto = oListaCarrito.Count;

              TempData["Venta"] = oVenta;
              TempData["DetalleVenta"] = detalle_venta;



            return Json(new { Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=code0001&status=true" }, JsonRequestBehavior.AllowGet);


         }



        [ValidarSession]
        [Authorize]

        public async Task<ActionResult> PagoEfectuado()
        {
            string idtransaccion = Request.QueryString["idTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);
           // string token = Request.QueryString["token"];

            /*CN_Paypal opaypal = new CN_Paypal();
            Response_Paypal<Response_Capture> response_Paypal = new Response_Paypal<Response_Capture>();
            response_Paypal = await opaypal.AprobarPago(token);*/

            ViewData["Status"] = status;

            if(status)
            {
                Venta oVenta =(Venta) TempData["Venta"];

                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];

                oVenta.idTransaccion = idtransaccion;

                string mensaje = string.Empty;

                bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

                ViewData["IdTransaccion"] = oVenta.idTransaccion;
            }
            return View();
        }


        [ValidarSession]
        [Authorize]
        public ActionResult MisCompras()
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;

            List<DetalleVenta> oLista = new List<DetalleVenta>();

            bool conversion;

            oLista = new CN_Venta().ListarCompras(idcliente).Select(oc => new DetalleVenta()
            {
                oProducto = new Producto()
                {
                   
                    nombre = oc.oProducto.nombre,
                    precio = oc.oProducto.precio,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.rutaImagen, oc.oProducto.nombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.nombreImagen)
                },
                cantidad = oc.cantidad,
                total = oc.total,
                idTransaccion = oc.idTransaccion
            }).ToList();
            return View(oLista);
        }

    }
}