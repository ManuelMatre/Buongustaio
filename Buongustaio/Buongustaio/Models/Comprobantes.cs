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
    
    public partial class Comprobantes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comprobantes()
        {
            this.Facturas = new HashSet<Facturas>();
            this.Retroalimentacion = new HashSet<Retroalimentacion>();
        }
    
        public string Folio { get; set; }
        public System.DateTime Fechayhora { get; set; }
        public string Orden { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public string Descuento { get; set; }
        public int TerminacionTC { get; set; }
        public int Transaccion { get; set; }
        public decimal Cliente { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Ordenes Ordenes { get; set; }
        public virtual Promociones Promociones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Retroalimentacion> Retroalimentacion { get; set; }
    }
}
