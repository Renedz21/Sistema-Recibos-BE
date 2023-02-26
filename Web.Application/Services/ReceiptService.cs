using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Services.Interfaces;
using Web.Domain.Entities;
using Web.Domain.Repositories;

namespace Web.Application.Services
{
    public class ReceiptService : IReceipt
    {
        private readonly IGenericRepository<Receipt> _receiptRepository;

        public ReceiptService(IGenericRepository<Receipt> receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public Task CreateReceipt(Receipt receipt)
        {
            throw new NotImplementedException();
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
