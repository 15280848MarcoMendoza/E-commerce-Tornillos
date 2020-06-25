using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class MetodosPago
    {
        public Dictionary<int, string> MetodoPago { get {
                return new Dictionary<int, string>() {
                    { 1, "Efectivo" },
                  //  { 2, "Debito" },
                  //  { 3, "Credito" }
                } ;
            }  }

        public int EFECTIVO => 1;
       // public int DEBITO => 2;
      //  public int CREDITO => 3;

    }
}