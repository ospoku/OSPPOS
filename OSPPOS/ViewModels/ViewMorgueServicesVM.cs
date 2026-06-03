using Microsoft.EntityFrameworkCore;

namespace DMX.ViewModels
{
    public class ViewMorgueServicesVM
    {
        public Guid MorgueServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Precision(4)]
        public decimal Amount { get; set; }
    }
}
