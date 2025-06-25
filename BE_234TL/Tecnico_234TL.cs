using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class Tecnico_234TL : Persona_234TL
    {
        public string Especialidad { get; set; }
        public bool Disponible { get; set; } = true;

        public Tecnico_234TL() : base() { }

        public Tecnico_234TL(string dni, string nombre, string apellido, string telefono, string especialidad): base(dni, nombre, apellido, telefono)
        {
            Especialidad = especialidad;
        }

        public override string ToString() => $"{base.InfoBasica} - Especialidad: {Especialidad}";
    }

}

