using BE_234TL;
using DAL_234TL;
using QuestPDF.Fluent;
using Servicios_234TL.Exception_234TL;

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
                throw new ValidacionesException_234TL("ReparacionNoEncontrada", "General", numeroReparacion);
            }
            if (!reparacion.Cobrado)
            {
                throw new ValidacionesException_234TL("DiagnosticoNoCobradoParaComprobante", "General", numeroReparacion);
            }
            if (reparacion.ComprobanteGenerado)
            {
                throw new ValidacionesException_234TL("ComprobanteYaGenerado", "General", numeroReparacion);
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
        public void GenerarPdf(ComprobanteIngreso_234TL comprobante)
        {
            string comprobantesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comprobantes");
            Directory.CreateDirectory(comprobantesPath);

            string filePath = Path.Combine(comprobantesPath, $"{comprobante.NumeroIngreso}.pdf");

            var document = new ComprobanteDocumentoBLL_234TL(comprobante);
            document.GeneratePdf(filePath);
        }
    }
}



