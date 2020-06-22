using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Carrito
    {
        public virtual Productos Productos { get; set; }
        public int Cantidad { get; set; }
    }
}