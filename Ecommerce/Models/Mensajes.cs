using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Mensajes
    {
        public int Id { get; set; }
        public string NombrePaqueteria { get; set; }
        public string FechaEntrega { get; set; }
        public string Direccion { get; set; }
        public string Remitente { get; set; }
        public string Cliente { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Peso { get; set; }
        public string Producto { get; set; }
        public virtual DetalleVenta DetalleVenta { get; set; }
       
    }
}