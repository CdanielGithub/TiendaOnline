using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carrito
    {
        public int idCarrito { set; get; }
        public Cliente oCliente { set; get; }
        public Producto oProducto { set; get; }
        public int cantidad { set; get; }
    }
}
