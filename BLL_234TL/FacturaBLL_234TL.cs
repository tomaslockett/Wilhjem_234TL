using BE_234TL;
using DAL_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class FacturaBLL_234TL : AbstractaBLL_234TL<Factura_234TL, int>
    {
        private readonly ReparacionBLL_234TL reparacionBLL = new();
        public FacturaBLL_234TL() : base(new FacturaDAL_234TL())
        {
        }
        public Factura_234TL CrearFacturaParaReparacion(int numeroReparacion, decimal total)
        {
            var reparacion = reparacionBLL.GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);

                if (reparacion == null)
                {
                    throw new ArgumentException("Mensaje_ReparacionNoEncontrada");
                }

                if (reparacion.FacturaGenerada)
                {
                    throw new InvalidOperationException("Mensaje_FacturaYaGenerada");
                }

                if (!reparacion.Cobrado)
                {
                    throw new InvalidOperationException("Mensaje_DiagnosticoNoCobrado");
                }

            var factura = new Factura_234TL
            {
                NumeroFactura = $"F-{numeroReparacion}",
                Reparacion = reparacion,
                Cliente = reparacion.Cliente,
                Total = total
            };

            Guardar(factura);

            reparacion.FacturaGenerada = true;
            reparacionBLL.Update(reparacion);

            return factura;
        }

    }
}
