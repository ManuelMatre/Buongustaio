//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Buongustaio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredientes
    {
        public string Id { get; set; }
        public string Lote { get; set; }
        public string Ingrediente { get; set; }
        public double Cantidad { get; set; }
        public System.DateTime Fecha_Compra { get; set; }
        public System.DateTime Fecha_Expiracion { get; set; }
    }
}
