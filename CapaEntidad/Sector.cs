using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Sector
    {
        public string idSector {  get; set; }
        public string Nombre { get; set; }
        public Ciudad oCiudad { get; set; }
    }

   /* public class Sector
    {
        public string idSector { get; set; }
        public string Nombre { get; set; }
        public int idCiudad { get; set; } // Cambiado a int
    }*/

}
