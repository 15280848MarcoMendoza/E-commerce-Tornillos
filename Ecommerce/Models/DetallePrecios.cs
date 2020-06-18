using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class DetallePrecios
    {

        public int Id { get; set; }
        public int Sku { get; set; }
        public string Producto { get; set; }
        public string  Precio_Anterior { get; set; }
        public string Precio_Nuevo { get; set; }
        public string Razon { get; set; }
        public string Fecha { get; set; }
    }
}