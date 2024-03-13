using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int idDetalleVenta { set; get; }
        public int idVenta { set; get; }
        public Producto oProducto { set; get; }
        public int cantidad { set; get; }
        public decimal total { set; get; }
        public string idTransaccion { set; get; }

    }
}
