using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_234TL
{
    public class Equipo_234TL
    {
        public Equipo_234TL() { }
        public string NumeroSerie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Estado { get; set; }

        public string FallaReportada { get; set; }

        public DateTime HoraIngreso { get; set; }
        public bool Desarmado { get; set; }

        public bool DañoVisible { get; set; }

        public Equipo_234TL(string numeroSerie, string marca, string modelo, string estado)
        {
            NumeroSerie = numeroSerie;
            Marca = marca;
            Modelo = modelo;
            Estado = estado;
        }
        public Equipo_234TL(string numeroSerie, string marca, string modelo, string estado, string fallaReportada, DateTime horaIngreso, bool desarmado, bool dañoVisible)
        {
            NumeroSerie = numeroSerie;
            Marca = marca;
            Modelo = modelo;
            Estado = estado;
            FallaReportada = fallaReportada;
            HoraIngreso = horaIngreso;
            Desarmado = desarmado;
            DañoVisible = dañoVisible;
        }

        public override string ToString() => $"{Marca} {Modelo} - Serie: {NumeroSerie}";
    }
}
