using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class DetalleVenta
    {
        public int Id{ get; set; }
        public virtual Productos Producto{ get; set; }
        public virtual Ventas Ventas { get; set; }
        public int Cantidad { get; set; }
        public int Porcentaje_Descuento { get; set; }
        public int Porcentaje_Incrmento { get; set; }
        public double Subtotal{ get; set; }


    }
}