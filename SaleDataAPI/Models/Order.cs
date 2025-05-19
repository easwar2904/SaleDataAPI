using Lumel_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumel_Assessment.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderId { get; set; } = default!;
        public DateTime DateOfSale { get; set; }
        public string Region { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public decimal ShippingCost { get; set; }
        public int CustomerId { get; set; }
        public string CustomerRefId { get; set; }
        public Customer Customer { get; set; } = default!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
