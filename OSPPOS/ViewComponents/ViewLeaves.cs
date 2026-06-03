using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewLeaves : ViewComponent
    {
        public readonly XContext dcx;
        public ViewLeaves(XContext dContext)
        {
            dcx = dContext;
        }
        public IViewComponentResult Invoke()
        {
            var lList = dcx.Leaves.Where(l => l.IsDeleted == false).Select(l => new ViewLeavesVM
            {
                LeaveId = l.LeaveId,
                CreatedDate = l.CreatedDate,
            }).OrderByDescending(t => t.CreatedDate).ToList();
            return View(lList);
        }
    }
}
