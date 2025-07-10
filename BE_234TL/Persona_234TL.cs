using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class Persona_234TL
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        public Persona_234TL() { }

        public Persona_234TL(string dni, string nombre, string apellido, string telefono)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
        }

        public virtual string NombreCompleto => $"{Nombre} {Apellido}";
        public virtual string InfoBasica => $"{NombreCompleto}";
    }
}
