using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Promociones
    {
        public string Periodo { get; set; }
        public string Tipo { get; set; }
        public string Producto { get; set; }
        public int Id { get; set; }


    }
   
}