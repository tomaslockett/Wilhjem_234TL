using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class Factura_234TL
    {
        public string NumeroFactura { get; set; }
        public Reparacion_234TL Reparacion { get; set; }
        public Cliente_234TL Cliente { get; set; }

        public Decimal Total { get; set; }

        public Factura_234TL() { }
        public Factura_234TL(string numeroFactura, Reparacion_234TL reparacion, Cliente_234TL cliente, Decimal total)
        {
            NumeroFactura = numeroFactura;
            Reparacion = reparacion;
            Cliente = cliente;
            Total = total;
        }
    }
}
