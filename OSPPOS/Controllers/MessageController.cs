using DMX.Data;
using DMX.Models;
using DMX.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DMX.Controllers
{
    public class MessageController(IMessageService messageService, UserManager<AppUser> userManager, XContext context) : Controller
    {
        readonly IMessageService ms = messageService;
        List<Message> oMessages = [];
        readonly UserManager<AppUser> usm = userManager;
        readonly XContext ctx = context;

        public IActionResult AllMessages()
        {
            return View();
        }
        public async Task<JsonResult> GetMessages(bool bIsGetOnlyRead = false)
        {

            var Receiver =  usm.FindByIdAsync(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Name")).Value);
            string ReceiverId =  Receiver.Result.UserName;
            oMessages = new List<Message>();
            oMessages = ms.GetMessages(ReceiverId, bIsGetOnlyRead);


            return Json(oMessages);
        }
        [HttpGet]
        public IActionResult UserMessages()
        {
            return ViewComponent(nameof(UserMessages));
        }

        //[HttpPost]
        //public JsonResult UpdateMessage(string Id)
        //{

        //    var msg = ctx.Messages.Where(m => m.MessageId == Id).FirstOrDefault();
        //    if (msg != null)
        //    {
        //        msg.MessageId = Id;
        //        msg.IsRead = 1;
        //        ctx.SaveChanges();
        //    }
        //    return Json(msg);
        //}
        [HttpPost]
        public async Task<IActionResult> UpdateMessage(string Id)
        {

            var msg = await ctx.Messages.Where(m => m.MessageId == Id).FirstOrDefaultAsync();
            if (msg != null)
            {
                msg.MessageId = Id;
                msg.IsRead = 1;
                 ctx.Entry(msg).State = EntityState.Modified;
              ctx.SaveChanges();
            }
            return RedirectToAction("UserMessages");
        }
    }
}
