﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reporte
    {
        public string fechaVenta {get; set;}
        public string cliente { get; set;}
        public string producto { get; set;}
        public decimal precio { get; set;}
        public int Cantidad { get; set;}
        public decimal Total { get; set;}
        public string idTransaccion { get; set;}
    }
}
