using System;
using BE_234TL; 
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class FacturaDocumentoBLL_234TL : IDocument
    {
        private readonly Factura_234TL _factura;
        public FacturaDocumentoBLL_234TL(Factura_234TL factura)
        {
            _factura = factura;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                page.Header().Element(ComposeHeader);

                page.Content().Element(ComposeContent);

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        }
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Factura #{_factura.NumeroFactura}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Fecha de Emisión: ").SemiBold();
                        text.Span($"{DateTime.Now:dd/MM/yyyy}");
                    });
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(30).Column(column =>
            {
                column.Spacing(20);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Border(1).Padding(10).Column(col =>
                    {
                        col.Item().Text("Facturar a:").FontSize(14).SemiBold();
                        col.Item().Text($"{_factura.Cliente.Nombre} {_factura.Cliente.Apellido}");
                        col.Item().Text($"DNI: {_factura.Cliente.Dni}");
                    });

                    row.ConstantItem(50);

                    row.RelativeItem(); 
                });

                column.Item().Element(ComposeTable);

                column.Item().AlignRight().Text($"Total a Pagar: ${_factura.Total:N2}").FontSize(16).Bold();
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4); 
                    columns.RelativeColumn();  
                    columns.RelativeColumn();  
                    columns.RelativeColumn();  
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Descripción");
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Precio");
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("Cantidad");
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Subtotal");
                });

                var descripcionServicio = $"Servicio de reparación para equipo con N° Serie: {_factura.Reparacion.Equipo.NumeroSerie}";

                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(descripcionServicio);
                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"${_factura.Total:N2}");
                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text("1");
                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"${_factura.Total:N2}");

            });
        }
    }
    
}
