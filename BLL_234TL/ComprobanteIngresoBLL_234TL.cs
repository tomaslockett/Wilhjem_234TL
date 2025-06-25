using BE_234TL;
using DAL_234TL;

namespace BLL_234TL
{
    public class ComprobanteIngresoBLL_234TL : AbstractaBLL_234TL<ComprobanteIngreso_234TL, string>
    {
        private readonly ReparacionBLL_234TL reparacionBLL = new();
        public ComprobanteIngresoBLL_234TL() : base(new ComprobanteIngresoDAL_234TL())
        {
        }

        public BE_234TL.ComprobanteIngreso_234TL CrearComprobanteParaReparacion(int numeroReparacion)
        {
            var reparacion = reparacionBLL.GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);


            if (reparacion == null)
            {
                throw new ArgumentException("Mensaje_NoSeEncontroReparacion");
            }
            if (!reparacion.Cobrado)
            {
                throw new InvalidOperationException("Mensaje_DiagnosticoNoCobrado");
            }
            if (reparacion.ComprobanteGenerado)
            {
                throw new InvalidOperationException("Mensaje_ComprobanteYaGenerado");
            }

            var comprobante = new BE_234TL.ComprobanteIngreso_234TL
            {
                NumeroIngreso = $"C-{(reparacion.NumeroReparacion).ToString()}",
                Reparacion = reparacion,
                Equipo = reparacion.Equipo,
                HoraIngreso = DateTime.Now
            };

            Guardar(comprobante);

            reparacion.ComprobanteGenerado = true;
            reparacionBLL.Update(reparacion);

            return comprobante;
        }
    }
}



