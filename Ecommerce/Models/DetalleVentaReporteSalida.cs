using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class DetalleVentaReporteSalida
    {
        public int Id { get; set; }
        public virtual ReporteSalida ReporteSalida { get; set; }
        public virtual DetalleVenta DetalleVenta { get; set; }
    }
}