//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Buongustaio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Retroalimentacion
    {
        public string Id { get; set; }
        public string Estado_orden { get; set; }
        public string Nivel_satisfaccion { get; set; }
        public decimal Cliente { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Orden { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Pedidos Pedidos { get; set; }
    }
}
