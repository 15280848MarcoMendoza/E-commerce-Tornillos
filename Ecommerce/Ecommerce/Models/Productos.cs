using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Productos
    {
        public int Id { set; get; }
        [StringLength(120,ErrorMessage ="Has excedido el tamaño permitido")] [Required(ErrorMessage = "Es nesesario el nombre")]
        public string Nombre { get; set; }
        [StringLength(120)] 
        public string Descripcion { get; set; }

        [StringLength(120)] 
        public string Url_image { get; set; }
        [StringLength(120)]
        public string Sabor { get; set; }
        [Range(0,100000,ErrorMessage ="El numero debe ser mayor a cero")]
        public bool activo { get; set; }
        [StringLength(120)] 
        public string Marca { get; set; }
        public double Costo_unitario { get; set; }
        public int Porcentage_descuento { get; set; }
        public int stock { get; set; }
        public int Status { get; set; }
        [Range(0,12,ErrorMessage = "El mes debe estar entre 1 y 12")]
        public int Time_Mount { get; set; }
        [Range(0,31,ErrorMessage ="El dia sdebe estar entre 1 y 31")]
        public int Time_Day { get; set; }
        public double Precio_final { get; set; }
        public Int32 Cantidad_ventas { get; set; }
        public virtual ICollection<Catalogos> Catalogos { set; get; }
        public virtual ICollection<DetalleVenta> DetalleVentas { set; get; }
        public virtual ICollection<DetalleCompras> DetalleCompra { set; get; }

        
    }
}