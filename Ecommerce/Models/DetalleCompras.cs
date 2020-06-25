using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Ecommerce.Models
{
    public class DetalleCompras
    {
        public int Id { get; set; }
        public virtual Productos Productos { get; set; }
        public int Cantidad { get; set; }
        public virtual Compras Compras { get; set; }
        public int PorcentajeDescuento { get; set; }
        public int PorcentajeIncremnto { get; set; }
        public double SubTotal { get; set; }
        public DateTime Fecha_vencimiento { get; set; }
    }
}