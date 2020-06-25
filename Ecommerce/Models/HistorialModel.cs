using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class HistorialModel
    {
        public Ventas venta { get; set; }
        public DetalleVenta detailVenta { get; set; }
    }
}