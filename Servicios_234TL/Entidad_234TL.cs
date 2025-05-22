using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL
{
    public class Entidad_234TL
    {
        public Entidad_234TL()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        //set;
    }
}