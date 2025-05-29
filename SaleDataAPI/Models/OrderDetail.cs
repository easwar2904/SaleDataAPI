using Lumel_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumel_Assessment.Models
{
    // Models/OrderDetail.cs
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderRefId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public string? ProductRefId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }

}
