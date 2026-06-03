using DMX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;



namespace DMX.Data
{
    public class XContext(DbContextOptions<XContext> options) : AuditableIdentityContext(options)
    {
        public DbSet<TransportType> TransportTypes { get; set; }
       public DbSet<CashLimit> CashLimits { get; set; }
        public DbSet<FeeStructure> FeeStructures { get; set; }
        public DbSet<MeetingAttendance> MeetingAttendance { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<DeceasedService> DeceasedServices { get; set; }
        public DbSet<LetterComment> LetterComments { get; set; }
        public DbSet<ServiceComment> ServiceComments { get; set; }    
     public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<DeceasedType> DeceasedTypes { get; set; }
        public DbSet<MaternityLeave> MaternityLeaves { get; set; }
        public DbSet<Deceased> Deceased { get; set; }
        public DbSet<Memo> Memos { get; set; }
        public DbSet<MemoAssignment> MemoAssignments { get; set; }
        public DbSet<PettyCash> PettyCash { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<PettyCashAssignment> PettyCashAssignments { get; set; }
        public DbSet<TravelRequestAssignment> TravelRequestAssignments { get; set; }
        public DbSet<TravelType> TravelTypes { get; set; }
        public DbSet<TravelRequest> TravelRequests { get; set; }
        public DbSet<MorgueService> MorgueServices { get; set; }
        public DbSet<Training> Trainings { get; set; }
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
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeceasedAssignment> DeceasedAssignments { get; set; }
        public DbSet<Status> Statuses { get; set; }
       public DbSet<Category> Categories { get; set; }
        public DbSet <Priority> Priorities { get; set; }
        public DbSet<Permission> Permissions { get; set; }
     
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<StockBatch> StockBatches => Set<StockBatch>();
        public DbSet<StockBatchItem> StockBatchItems => Set<StockBatchItem>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<SaleOrder> SaleOrders => Set<SaleOrder>();
        public DbSet<SaleOrderItem> SaleOrderItems => Set<SaleOrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();




    }
}
