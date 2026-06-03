using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class UserMessages(UserManager<AppUser> userManager, XContext context):ViewComponent
    {
        public readonly UserManager<AppUser> usm= userManager;
        public readonly XContext xtc= context;
        public IViewComponentResult Invoke()
        {
            var userMessages = xtc.Messages.Where(m => m.Reciever == usm.GetUserAsync(HttpContext.User).Result.Id & m.IsRead == 0).Select(m => new UserMessagesVM
            {

                MessageId = m.MessageId,
                Sender = m.Sender,
                Body = m.Body,
                IsRead = m.IsRead,
                Subject = m.Subject,
                Reciever = m.Reciever,
                CreatedDate = m.CreatedDate.Value,
            }).
            
                Take(5).ToList();
       
            return View(userMessages);
        }
    }
}
