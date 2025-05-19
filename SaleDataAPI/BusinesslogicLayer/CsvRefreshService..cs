using CsvHelper;
using CsvHelper.Configuration;
using Lumel_Assessment.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace SaleDataAPI.BusinesslogicLayer
{
    public class CsvRefreshService : ICsvRefreshService
    {
        private readonly SalesDbContext db;
        private readonly ILogger<CsvRefreshService> _logger;

        public CsvRefreshService(SalesDbContext dbContext, ILogger<CsvRefreshService> logger)
        {
            db = dbContext;
            _logger = logger;
        }

        public async Task RefreshDataAsync(CancellationToken cancellationToken = default)
        {
            var filePath = "C:\\Users\\muthu\\Downloads\\new sales data.csv"; // Or inject via config

            try
            {
                //using var reader = new StreamReader(filePath);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.Trim(),
                    HeaderValidated = null,
                    MissingFieldFound = null,
                };

                using var reader = new StreamReader(filePath, Encoding.UTF8);
                using var csv = new CsvReader(reader, config);
                var records = csv.GetRecords<SaleCsvRecord>().ToList();

                foreach (var r in records)
                {
                    // Check if order exists
                    var customer = await db.Customers
               .FirstOrDefaultAsync(c => c.CustomerId == r.CustomerID, cancellationToken);


                  
                    if (customer == null)
                    {
                        customer = new Customer
                        {
                            CustomerId = r.CustomerID,
                            Name = r.CustomerName,
                            Email = r.CustomerEmail,
                            Address = r.CustomerAddress
                        };
                        db.Customers.Add(customer);
                        await db.SaveChangesAsync(cancellationToken);
                    }

                    // Get or Add Product
                    var product = await db.Products
               .FirstOrDefaultAsync(p => p.ProductId == r.ProductID, cancellationToken);

                    if (product == null)
                    {
                        product = new Product
                        {
                            ProductId = r.ProductID,
                            Name = r.ProductName,
                            Category = r.Category
                        };
                        db.Products.Add(product);
                        await db.SaveChangesAsync(cancellationToken);
                    }

                    // Get or Add Order
                    var existingOrder = await db.Orders
               .FirstOrDefaultAsync(o => o.OrderId == r.OrderID, cancellationToken);
                    if (existingOrder != null)
                        continue; // skip duplicates
            
                        var order = new Order
                        {
                            OrderId = r.OrderID,
                            CustomerRefId = r.CustomerID,
                            DateOfSale = r.DateOfSale,
                            PaymentMethod = r.PaymentMethod,
                            Region = r.Region,
                            ShippingCost = r.ShippingCost,
                            CustomerId = customer.Id
                        };
                        db.Orders.Add(order);
                        await db.SaveChangesAsync(cancellationToken);
                    

                    // Add OrderDetail
                    var orderDetail = new OrderDetail
                    {
                        OrderRefId = Convert.ToInt32(r.OrderID),
                        ProductRefId = r.ProductID,
                        QuantitySold = r.QuantitySold,
                        UnitPrice = r.UnitPrice,
                        Discount = r.Discount,
                        ProductId = product.Id,
                        OrderId = order.Id
                    };
                    db.OrderDetails.Add(orderDetail);


                    await db.SaveChangesAsync(cancellationToken);
                    Console.WriteLine("CSV import completed successfully.");
                }

                await db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Data refresh completed successfully at {Time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Data refresh failed at {Time}", DateTime.UtcNow);
                throw;
            }
        }
    }

}
