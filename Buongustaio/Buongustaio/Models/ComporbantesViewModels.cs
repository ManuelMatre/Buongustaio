using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buongustaio.Models
{
    public class registrarComprobante{
        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Num de Tarjeta: ")]
        public int numTarjeta { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Fecha de Expiracion: ")]
        public int fechaExpriacion { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre de Propietario: ")]
        public int Nombre { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Clave de Seguridad: ")]
        public int claveSeguridad { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Cantidad: ")]
        public int Cantidad { get; set; }
    }
}