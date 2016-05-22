using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buongustaio.Classes
{
    public class MiPedido
    {
        public string Id { get; set; }
        public decimal Cliente { get; set; }
        public string Pedido { get; set; }
        public DateTime Fecha { get; set; }
        public double PagoTotal { get; set; }
    }
}