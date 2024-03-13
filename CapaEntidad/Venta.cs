using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int idVenta { set; get; }
        public int idCliente { set; get; }
        public int totalProducto { set; get; }
        public decimal montoTotal { set; get; }
        public string contacto { set; get; }
        public string idDistrito { set; get; }
        public string telefono { set; get; }
        public string direccion { set; get; }
        public string fechaVenta { set; get; }
        public string idTransaccion { set; get; }

        public List<DetalleVenta> oDetalleVenta { set; get; }
    }
}
