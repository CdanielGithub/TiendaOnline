using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int idCliente {set; get;}
        public string nombres { set; get;}
        public string apellidos { set; get;}
        public string correo { set; get;}
        public string clave { set; get;}
        public string ConfirmarClave { set; get; }
        public bool restablecer { set; get; }
    }
}
