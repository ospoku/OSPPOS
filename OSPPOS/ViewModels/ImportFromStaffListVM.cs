
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class ImportFromStaffListVM
    {
        public IEnumerable<object> SelectedId { get; internal set; }
        public SelectList StaffList { get; internal set; }
    }
}
