using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewComponents
{
    public class AddFeeStructure(XContext context) :ViewComponent
    {
        public readonly XContext dcx = context;
        public IViewComponentResult Invoke()
        {

            return View(new AddFeeStructureVM
            { 
                DeceasedTypes = new SelectList(dcx.DeceasedTypes.ToList(), nameof(DeceasedType.DeceasedTypeId), nameof(DeceasedType.Name)) });
        }
    }
}
