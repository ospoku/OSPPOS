using OSPPOS.Models;

namespace OSPPOS.ViewModels
{
    public class DebtorReportVM
    {
        public List<DebtorAgingVM> Debtors { get; set; } = [];
        public decimal TotalOutstanding => Debtors.Sum(d => d.TotalOwed);
    }

    public class DebtorAgingVM
    {
        public Customer Customer { get; set; } = null!;
        public decimal Current { get; set; }      // 0-30 days
        public decimal Days30 { get; set; }       // 31-60
        public decimal Days60 { get; set; }       // 61-90
        public decimal Days90Plus { get; set; }   // 90+
        public decimal TotalOwed => Current + Days30 + Days60 + Days90Plus;
    }

}
