using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application.Services.Interfaces;
using Web.Domain.Entities;
using Image = iTextSharp.text.Image;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceipt _receipt;

        public ReceiptController(IReceipt receiptService)
        {
            _receipt = receiptService;
        }

        // GET: api/<ReceiptController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Receipt>> Get()
        {
            var receipts = await _receipt.GetReceipts();
            return receipts;
        }

        // GET api/<ReceiptController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReceiptController>
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GeneratePdf(Receipt receipt)
        {
            var newReceipt = new Receipt
            {
                Logo = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                Currency = receipt.Currency,
                Amount = receipt.Amount,
                Description = receipt.Description,
                Address = receipt.Address,
                Name = receipt.Name,
                DocumentType = receipt.DocumentType,
                DocumentNumber = receipt.DocumentNumber
            };

            MemoryStream ms = new();
            Document document = new(PageSize.A4, 25, 25, 30, 30);
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

            //Image imagenLogo = Image.GetInstance(new WebClient().DownloadData(receipt.Logo));
            //imagenLogo.Alignment = Element.ALIGN_CENTER;
            //logoCell.AddElement(imagenLogo);
            logoTituloTabla.AddCell(logoCell);

            /**/

            //Descargar la imagen desde la URL
            
            

            // Crear una instancia de Image usando los bytes de la imagen descargada
            
            image.Alignment = Element.ALIGN_CENTER;
            image.ScalePercent(100f);
            document.Add(image);


            /**/

            // Agregar el título
            PdfPCell tituloCell = new PdfPCell();
            tituloCell.Border = Rectangle.NO_BORDER;
            tituloCell.PaddingLeft = 10;
            tituloCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            
            Paragraph titulo = new Paragraph(receipt.Title);
            titulo.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
            tituloCell.AddElement(titulo);
            logoTituloTabla.AddCell(tituloCell);

            document.Add(titulo);
            document.Add(Chunk.NEWLINE);

            // Agregar los datos a la tabla
            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;

            // Agregar el encabezado y los datos
            tabla.AddCell("Nombre");
            tabla.AddCell(receipt.Name);
            tabla.AddCell("Descripción");
            tabla.AddCell(receipt.Description);
            tabla.AddCell("Número de documento");
            tabla.AddCell(receipt.DocumentNumber);

            // Ajustar el ancho de las columnas de la tabla
            float[] anchurasColumnas = new float[] { 1f, 1f };
            tabla.SetWidths(anchurasColumnas);

            // Agregar la tabla al documento
            document.Add(tabla);


            // Descargar la imagen desde la URL
            //WebClient client = new WebClient();
            //byte[] imageData = client.DownloadData(newReceipt.Logo);

            //// Crear una instancia de Image usando los bytes de la imagen descargada
            //iTextSharp.text.Image image = Image.GetInstance(imageData);
            //image.Alignment = Element.ALIGN_CENTER;
            //image.ScalePercent(100f);
            //document.Add(image);

            //newReceipt.Title = receipt.Title;

            //Paragraph title = new Paragraph(newReceipt.Title, new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
            //title.Alignment = Element.ALIGN_CENTER;
            //document.Add(title);


            //PdfPTable table = new(2);

            //table.AddCell("Celda 1, fila 1");

            //var text = 
            //    $"Currency: {newReceipt.Currency} \n" +
            //    $"Amount: {newReceipt.Amount} \n" +
            //    $"Description: {newReceipt.Description} \n" +
            //    $"Address: {newReceipt.Address} \n" +
            //    $"Name: {newReceipt.Name} \n" +
            //    $"DocumentType: {newReceipt.DocumentType} \n" +
            //    $"DocumentNumber: {newReceipt.DocumentNumber} \n";
           
            //document.Add(table);
            
            document.Close();
            writer.Close();

            //var filename = newReceipt.Title;
            return File(ms.ToArray(), "application/pdf", "archivo.pdf");
            //return new FileContentResult(ms.ToArray(), "application/pdf");
        }

        // PUT api/<ReceiptController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReceiptController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
