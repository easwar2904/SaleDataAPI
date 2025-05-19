using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
namespace Lumel_Assessment.Models
{
    // Models/SaleCsvRecord.cs
    

    public class SaleCsvRecord
    {
        [Name("Order ID")]
        public string OrderID { get; set; } = default!;

        [Name("Product ID")]
        public string ProductID { get; set; } = default!;

        [Name("Customer ID")]
        public string CustomerID { get; set; } = default!;

        [Name("Product Name")]
        public string ProductName { get; set; } = default!;

        [Name("Category")]
        public string Category { get; set; } = default!;

        [Name("Region")]
        public string Region { get; set; } = default!;

        [Name("Date of Sale")]
        public DateTime DateOfSale { get; set; }

        [Name("Quantity Sold")]
        public int QuantitySold { get; set; }

        [Name("Unit Price")]
        public decimal UnitPrice { get; set; }

        [Name("Discount")]
        public decimal Discount { get; set; }

        [Name("Shipping Cost")]
        public decimal ShippingCost { get; set; }

        [Name("Payment Method")]
        public string PaymentMethod { get; set; } = default!;

        [Name("Customer Name")]
        public string CustomerName { get; set; } = default!;

        [Name("Customer Email")]
        public string CustomerEmail { get; set; } = default!;

        [Name("Customer Address")]
        public string CustomerAddress { get; set; } = default!;
    }


}
