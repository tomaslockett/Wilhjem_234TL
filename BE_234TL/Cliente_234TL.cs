using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class Cliente_234TL : Persona_234TL
    {
        public Cliente_234TL() : base() { }

        public Cliente_234TL(string dni, string nombre, string apellido, string telefono): base(dni, nombre, apellido, telefono) { }

        public override string ToString() => InfoBasica;
    }
}
