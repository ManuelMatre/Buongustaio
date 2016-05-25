using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buongustaio.Classes
{
    public class MiPago
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string Token { get; set; }
        public double Amount { get; set; }
    }
}