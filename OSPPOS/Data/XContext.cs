
using DMX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;



namespace OSPPOS.Data
{
    public class XContext(DbContextOptions<XContext> options) : AuditableIdentityContext(options)
    {
        
        public DbSet<FeeStructure> FeeStructures { get; set; }
     
        public DbSet<Message> Messages { get; set; }
      
   
     public DbSet<RequestType> RequestTypes { get; set; }
      
      
        
    
       
       
        
    
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
