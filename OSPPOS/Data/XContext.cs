
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;



namespace OSPPOS.Data
{
    public class XContext(DbContextOptions<XContext> options) : AuditableIdentityContext(options)
    {
        
        public DbSet<FeeStructure> FeeStructures { get; set; }
        public DbSet<MeetingAttendance> MeetingAttendance { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<DeceasedService> DeceasedServices { get; set; }
        public DbSet<LetterComment> LetterComments { get; set; }
        public DbSet<ServiceComment> ServiceComments { get; set; }    
     public DbSet<RequestType> RequestTypes { get; set; }
      
        public DbSet<DeceasedType> DeceasedTypes { get; set; }
    
        public DbSet<Memo> Memos { get; set; }
        public DbSet<MemoAssignment> MemoAssignments { get; set; }
        public DbSet<PettyCash> PettyCash { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<PettyCashAssignment> PettyCashAssignments { get; set; }
        
        public DbSet<MorgueService> MorgueServices { get; set; }
        
        public DbSet<ExcuseDuty> ExcuseDuties { get; set; }
        public DbSet<SMSTask> SMSTasks { get; set; }
        public DbSet<MemoComment> MemoComments { get; set; }
        public DbSet<ExcuseDutyComment> ExcuseDutyComments { get; set; }
        public DbSet<LeaveComment> LeaveComments { get; set; }
        public DbSet<DeceasedComment> PatientComments { get; set; }
        public  DbSet<PettyCashComment> PettyCashComments { get; set; }
        public DbSet<LetterAssignment> LetterAssignments { get; set; }
        public DbSet<TravelRequestComment> TravelRequestComments { get; set;}
        public DbSet <MaternityLeaveComment> MaternityLeaveComments { get; set; }
        public DbSet<ExcuseDutyAssignment> ExcuseDutyAssignments { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<DeceasedAssignment> PatientAssignments { get; set; }
        public DbSet<ServiceAssignment> ServiceAssignments { get; set; }
        public DbSet <SickAssignment> SickAssignments { get; set; }
        public DbSet<PerDiem> PerDiems { get; set; }
       
        
        public DbSet<Status> Statuses { get; set; }
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
