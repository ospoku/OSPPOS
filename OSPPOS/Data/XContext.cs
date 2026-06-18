
using DMX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;



namespace OSPPOS.Data
{
    public class XContext(DbContextOptions<XContext> options) : AuditableIdentityContext(options)
    {
        
 
     
        public DbSet<Message> Messages { get; set; }
      
 
      
      public DbSet<PaymentMethod> PaymentMethods { get; set; }
        
    public DbSet<PaymentStatus> PaymentStatuses { get; set; }
       public DbSet<SaleType> SaleTypes { get; set; } 
       public DbSet<Unit> Units { get; set; }
        
    
       public DbSet<Category> Categories { get; set; }
      
        public DbSet<Permission> Permissions { get; set; }
     
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockBatch> StockBatches { get; set; }
        public DbSet<StockBatchItem> StockBatchItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderItem> SaleOrderItems { get; set; }    
        public DbSet<Payment> Payments { get; set; }




    }
}
