using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class VentaProductoDetalle
    {
        public DetalleVenta detailVenta { get; set; }
        public Productos detailProducto { get; set; }
    }
}