using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ProductoCatalogo
    {
       
            public int Id { get; set; }
            public int ProductoId { get; set; }
            public int CatalogoId { get; set; }

            public virtual ICollection<Productos> Productos{ get; set; }
            public virtual ICollection<Catalogos> Catalogos{ get; set; }
        }
    
}