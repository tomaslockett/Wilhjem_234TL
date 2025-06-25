using BE_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class ReparacionDTOBLL_234TL
    {
        private ReparacionBLL_234TL bLL_234TL = new();
        public List<ReparacionDTO_234TL> ObtenerReparacionesDTO()
        {
            
            var reparaciones = bLL_234TL.GetAll();
            var resultado = new List<ReparacionDTO_234TL>();

            foreach (var r in reparaciones)
            {
                resultado.Add(new ReparacionDTO_234TL
                {
                    NumeroReparacion = r.NumeroReparacion.ToString(),
                    Estado = r.Estado,
                    NumeroSerie = r.Equipo.NumeroSerie,
                    NombreCliente = $"{r.Cliente.Nombre} {r.Cliente.Apellido}",
                    DNICliente = r.Cliente.Dni,
                    NombreTecnico = $"{r.Tecnico.Nombre} {r.Tecnico.Apellido}",
                    EspecialidadTecnico = r.Tecnico.Especialidad,
                    Cobrado = r.Cobrado,
                    FacturaGenerada = r.FacturaGenerada,
                    ComprobanteGenerado = r.ComprobanteGenerado
                });
            }

            return resultado;
        }
    }
}
