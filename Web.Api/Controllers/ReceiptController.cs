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
        public async Task<IActionResult> GeneratePdf(Receipt receipt)
        {
            return await this._receipt.CreateReceipt(receipt);
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
