
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
    
public partial class Retroalimentacion
{

    public string Id { get; set; }

    public string Estado_orden { get; set; }

    public string Nivel_satisfaccion { get; set; }

    public decimal Cliente { get; set; }

    public System.DateTime Fecha { get; set; }

    public string Orden { get; set; }



    public virtual Clientes Clientes { get; set; }

    public virtual Comprobantes Comprobantes { get; set; }

}

}
