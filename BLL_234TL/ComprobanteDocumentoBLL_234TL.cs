using BE_234TL;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class ComprobanteDocumentoBLL_234TL : IDocument
    {
        private readonly ComprobanteIngreso_234TL _comprobante;

        public ComprobanteDocumentoBLL_234TL(ComprobanteIngreso_234TL comprobante)
        {
            _comprobante = comprobante;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5); 
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x => x.Span("Gracias por confiar en nosotros."));
            });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(18).SemiBold().FontColor(Colors.Blue.Darken2);

            container.Column(column =>
            {
                column.Item().Text("Comprobante de Ingreso").Style(titleStyle);
                column.Item().Text($"N° de Ingreso: {_comprobante.NumeroIngreso}").FontSize(10);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(25).Column(column =>
            {
                column.Spacing(15);


                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Fecha y Hora de Ingreso").SemiBold();

                        col.Item().Text($"{_comprobante.HoraIngreso:dd/MM/yyyy HH:mm:ss 'hs'}");
                    });
                });

                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);


                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Detalles del Equipo Recibido").SemiBold();
                        col.Item().Text($"Número de Serie: {_comprobante.Equipo.NumeroSerie}");
                        col.Item().Text($"Marca: {_comprobante.Equipo.Marca}");
                        col.Item().Text($"Modelo: {_comprobante.Equipo.Modelo}");
                        col.Item().Text($"Falla Reportada: {_comprobante.Reparacion.Equipo.FallaReportada}");
                    });
                });
            });
        }
    }
}

