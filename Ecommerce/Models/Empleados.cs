using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Empleados
    {
        public int Id { get; set; }
        public string Id_users { get; set; }
        public string Nombre { get; set; }
        public bool Sexo { get; set; }
        public double Salario { get; set; }
        public string Puesto { get; set; }
        public string Area { get; set; }
        public DateTime Fecha_Nacimeinto { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public int CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public int NoInterior { get; set; }
        public int NoExterior { get; set; }
        public string Referencia { get; set; }
        public bool Registro_Completo { get; set; }
        
        [DefaultValue(true)]
        public bool Active { get; set; }
    }
}