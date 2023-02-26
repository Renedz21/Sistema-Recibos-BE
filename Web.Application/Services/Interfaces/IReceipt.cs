using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Application.Services.Interfaces
{
    public interface IReceipt
    {
        Task<List<Receipt>> GetReceipts();

        Task<Receipt> GetReceipt(int id);

        Task CreateReceipt(Receipt receipt);
    }
}
