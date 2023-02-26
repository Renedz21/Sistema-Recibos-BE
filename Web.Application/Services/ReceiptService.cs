using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Reflection.Metadata;
using Web.Application.Services.Interfaces;
using Web.Domain.Entities;
using Web.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Web.Application.Services
{
    public class ReceiptService : IReceipt
    {
        private readonly IGenericRepository<Receipt> _receiptRepository;

        public ReceiptService(IGenericRepository<Receipt> receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<IActionResult> CreateReceipt(Receipt receipt)
        {
            var newReceipt = new Receipt
            {
                Logo = receipt.Logo,
                Currency = receipt.Currency,
                Amount = receipt.Amount,
                Description = receipt.Description,
                Address = receipt.Address,
                Name = receipt.Name,
                DocumentType = receipt.DocumentType,
                DocumentNumber = receipt.DocumentNumber
            };

            MemoryStream ms = new();
            iTextSharp.text.Document document = new(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            // Agregar el título al inicio con el logo
            PdfPTable logoTituloTabla = new(2);
            logoTituloTabla.WidthPercentage = 100;
            logoTituloTabla.HorizontalAlignment = Element.ALIGN_CENTER;

            // Agregar el logo
            PdfPCell logoCell = new PdfPCell();
            WebClient client = new WebClient();

            logoCell.Border = Rectangle.NO_BORDER;

            byte[] imageData = client.DownloadData(newReceipt.Logo);

            iTextSharp.text.Image image = Image.GetInstance(imageData);

            logoTituloTabla.AddCell(logoCell);

            image.Alignment = Element.ALIGN_CENTER;
            image.ScalePercent(100f);
            document.Add(image);

            PdfPCell tituloCell = new PdfPCell();
            tituloCell.Border = Rectangle.NO_BORDER;
            tituloCell.PaddingLeft = 10;
            tituloCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Agregar el título
            Paragraph titulo = new Paragraph(receipt.Title, new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            tituloCell.AddElement(titulo);
            logoTituloTabla.AddCell(tituloCell);

            document.Add(titulo);
            document.Add(Chunk.NEWLINE);

            // Agregar los datos a la tabla
            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;

            // Agregar el encabezado y los datos
            tabla.AddCell("Nombres");
            tabla.AddCell(receipt.Name);
            tabla.AddCell("Dirección");
            tabla.AddCell(receipt.Address);
            tabla.AddCell("Tipo de Moneda");
            tabla.AddCell(receipt.Currency);
            tabla.AddCell("Monto a Pagar");
            tabla.AddCell(receipt.Amount);
            tabla.AddCell("Tipo de documento");
            tabla.AddCell(receipt.DocumentType);
            tabla.AddCell("Número de documento");
            tabla.AddCell(receipt.DocumentNumber);
            tabla.AddCell("Descripción");
            tabla.AddCell(receipt.Description);

            // Ajustar el ancho de las columnas de la tabla
            float[] anchurasColumnas = new float[] { 1f, 1f };
            tabla.SetWidths(anchurasColumnas);

            // Agregar la tabla al documento
            document.Add(tabla);

            document.Close();
            writer.Close();

            await _receiptRepository.Add(receipt);

            //return File(ms.ToArray(), "application/pdf", "archivo.pdf");
            return new FileContentResult(ms.ToArray(), "application/pdf");
        }

        public Task<Receipt> GetReceipt(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Receipt>> GetReceipts()
        {
            return await _receiptRepository.FindAll();
        }
    }
}
