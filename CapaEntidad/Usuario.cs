using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int idUsuario { set; get; }
        public string nombre { set; get; }
        public string apellidos { set; get; }
        public string correo { set; get; }
        public string clave { set; get; }
        public bool reestablecer { set; get; }
        public bool activo { set; get; }
    }
}
