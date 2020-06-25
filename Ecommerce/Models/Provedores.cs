using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Provedores
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Es nesesario el nombre"), StringLength(120)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Es nesesario el telefono"), StringLength(14),RegularExpression("[+]{0,1}[0-9]{0,3}[0-9]{10}", ErrorMessage = "Ingrese un telefono valido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Es nesesario ingresar un correo"), StringLength(320),RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Ingrese un correo valido")]
        public string Correo { get; set; }
        public virtual ICollection<Compras> Compras { get; set; }

    }
}