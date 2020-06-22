using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ReporteSalida
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string Fecha { get; set; }
        public string Producto { get; set; }
        public string Cantidad { get; set; }
        public virtual ICollection<DetalleVentaReporteSalida> DetalleVentaReporteSalida { get; set; }
    }
}