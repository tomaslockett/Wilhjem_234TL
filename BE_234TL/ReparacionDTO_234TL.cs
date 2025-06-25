using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class ReparacionDTO_234TL
    {
        public string NumeroReparacion { get; set; }
        public string Estado { get; set; }
        public string NumeroSerie { get; set; }
        public string NombreCliente { get; set; }
        public string DNICliente { get; set; }
        public string NombreTecnico { get; set; }
        public string EspecialidadTecnico { get; set; }

        public bool Cobrado { get; set; }
        public bool FacturaGenerada { get; set; } 
        public bool ComprobanteGenerado { get; set; } 
    }
}
