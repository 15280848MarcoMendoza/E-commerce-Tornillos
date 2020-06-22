using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Cliente
    {
     
        [Key] 
        public int Id { get; set; }
        public string Id_users { get; set; }
        public string Nombre { get; set; }
        public bool Sexo { get; set; }
        public DateTime Fecha_Nacimeinto { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public int CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public int NoInterior { get; set; }
        public int NoExterior { get; set; }
        public string Referencia { get; set; }
        public string Tipo_targeta { get; set; }
        public string No_targeta { get; set; }
        
        [DefaultValue(true)]
        public bool Active { get; set; }
        public virtual ICollection<Ventas> Ventas { get; set; }

    }
}