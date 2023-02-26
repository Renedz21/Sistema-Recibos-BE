using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        public String Logo { get; set; }
        public String Currency { get; set; }
        public String Amount { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Address { get; set; }
        public String Name { get; set; }
        public String DocumentType { get; set; }
        public String DocumentNumber { get; set; }
    }
}
