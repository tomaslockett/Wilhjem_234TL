using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Singleton_234TL
{
    public class Exepcion_234TL : Exception
    {
        public Resultados_234TL Resultados;

        public Exepcion_234TL(Resultados_234TL resultados)
        {
            Resultados = resultados;
        }
    }
}