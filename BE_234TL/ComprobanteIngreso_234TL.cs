using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class ComprobanteIngreso_234TL
    {
        public string NumeroIngreso { get; set; }
        public Reparacion_234TL Reparacion { get; set; }
        public Equipo_234TL Equipo { get; set; }
        public DateTime HoraIngreso { get; set; } = DateTime.Now;

        public ComprobanteIngreso_234TL() { }   

        public ComprobanteIngreso_234TL(string numeroComprobante, Reparacion_234TL reparacion, Equipo_234TL equipo)
        {
            NumeroIngreso = numeroComprobante;
            Reparacion = reparacion;
            Equipo = equipo;
        }


    }
}
