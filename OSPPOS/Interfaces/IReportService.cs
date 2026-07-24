using OSPPOS.ViewModels;

namespace OSPPOS.Interfaces
{
   
        public interface IReportService
        {
      
            Task<DashboardVM> GetDashboardAsync();
        
        }
    }

