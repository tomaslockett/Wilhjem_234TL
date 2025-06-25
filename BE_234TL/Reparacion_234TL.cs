using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class Reparacion_234TL
    {
        public int NumeroReparacion { get; set; }
        public string Estado { get; set; } //Pendiente En Proceso Completada
        public Cliente_234TL Cliente { get; set; } 
        public Equipo_234TL Equipo { get; set; }
        public Tecnico_234TL Tecnico { get; set; }
        public bool Cobrado { get; set; }  
        public bool FacturaGenerada { get; set; } 
        public bool ComprobanteGenerado { get; set; } 

        public Reparacion_234TL() { }
        public Reparacion_234TL(int numeroReparacion, string estado, Cliente_234TL cliente, Equipo_234TL equipo, Tecnico_234TL tecnico)
        {
            NumeroReparacion = numeroReparacion;
            Estado = estado;
            Cliente = cliente;
            Equipo = equipo;
            Tecnico = tecnico;
        }
        public Reparacion_234TL(int numeroReparacion,string estado,Cliente_234TL cliente,Equipo_234TL equipo,Tecnico_234TL tecnico,bool cobrado,bool facturaGenerada,bool comprobanteGenerado)
        {
            NumeroReparacion = numeroReparacion;
            Estado = estado;
            Cliente = cliente;
            Equipo = equipo;
            Tecnico = tecnico;
            Cobrado = cobrado;
            FacturaGenerada = facturaGenerada;
            ComprobanteGenerado = comprobanteGenerado;
        }

    }
}
