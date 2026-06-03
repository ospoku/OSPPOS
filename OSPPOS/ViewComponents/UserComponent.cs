using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;




namespace DMX.ViewComponents
{
    public class UserComponent  (UserManager < AppUser > userManager): ViewComponent
    {

        public readonly UserManager<AppUser> usm = userManager;
        public IViewComponentResult Invoke()
        {

            var user = usm.GetUserAsync(HttpContext.User).Result;
            string base64Image = string.Empty;
            string userInitials = string.Empty;
            if (user.Picture != null)
            {
                base64Image = Convert.ToBase64String(user.Picture);
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Firstname) && !string.IsNullOrEmpty(user.Lastname))
                {
                    userInitials = $"{user.Firstname[0]}{user.Lastname[0]}".ToUpper();
                }
                else if (!string.IsNullOrEmpty(user.Firstname))
                {
                    userInitials = user.Firstname[0].ToString().ToUpper();
                }
                else if (!string.IsNullOrEmpty(user.Lastname))
                {
                    userInitials = user.Lastname[0].ToString().ToUpper();
                }

            }

            UserComponentVM userComponentVM = new()
            {
                userWelcome = user.UserName,
                userInitials = userInitials,
              userPicture=base64Image,
            };


        return View(userComponentVM);
        
        }
    }
    
}

