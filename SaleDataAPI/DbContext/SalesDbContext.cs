using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumel_Assessment.Models;
using Microsoft.Extensions.Logging;



public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options): base(options)
    {
    }
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    private readonly ILogger _logger;
    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    try
    //    {

    //        options.UseMySql(
    //            "Server=localhost;Database=SalesDb;User=root;Password=Easwar@9960;",
    //            new MySqlServerVersion(new Version(8, 0, 34))
    //        );
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Failed while establishing DB Connection {Time}", DateTime.UtcNow);
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasIndex(c => c.CustomerId).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.ProductId).IsUnique();
        modelBuilder.Entity<Order>().HasIndex(o => o.OrderId).IsUnique();
    }
}

