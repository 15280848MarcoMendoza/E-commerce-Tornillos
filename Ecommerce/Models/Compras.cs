using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Compras
    {
        public int DEBITO => 1;
        public int CREDITO => 2;
        public int STATUS_PEDIDO => 1;
        public int STATUS_PAGADO => 2;
        public int STATUS_RECIBIDO => 3;
        public int Id { get; set; }
        public virtual ICollection<DetalleCompras> DetallesCompras { get; set; }
        public virtual Provedores Provedores { get; set; }
        public DateTime FechaCompra { get; set; }
        public int Status { get; set; }
        public int TipoPago { get; set; }
        public double Total { get; set; }

    }
}