using BE_234TL;
using DAL_234TL;
using QuestPDF.Fluent;
using Servicios_234TL.Exception_234TL;

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
                throw new ValidacionesException_234TL("ReparacionNoEncontrada", "General", numeroReparacion);
            }

            if (reparacion.FacturaGenerada)
            {
                throw new ValidacionesException_234TL("FacturaYaGenerada", "General", numeroReparacion);
            }

            if (!reparacion.Cobrado)
            {
                throw new ValidacionesException_234TL("DiagnosticoNoCobradoParaFactura", "General", numeroReparacion);
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

        public void GenerarPdf(Factura_234TL factura)
        {
            string facturasPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Facturas");
            Directory.CreateDirectory(facturasPath); 

            string filePath = Path.Combine(facturasPath, $"{factura.NumeroFactura}.pdf");

            var document = new FacturaDocumentoBLL_234TL(factura);

            document.GeneratePdf(filePath);
        }

    }
}
