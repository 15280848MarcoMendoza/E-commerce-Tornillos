using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Catalogos
    {

        public int Id { get; set; }
        public string name { get; set; }

        public virtual ICollection<Productos> Productos {get; set;}
        
    }
}