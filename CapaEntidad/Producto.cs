﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set;}
        public decimal precio { get; set; }
        public string PrecioTexto {  get; set; }
        public int stock { get; set; }
        public string rutaImagen { get; set; }
        public string nombreImagen { get; set; }
        public bool activo { get; set; }

        public string Base64 { get; set; }// para visualizar la imagen

        public string Extension { get; set; }
        
    }
}
