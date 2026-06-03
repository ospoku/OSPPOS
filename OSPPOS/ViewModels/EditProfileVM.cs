using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class EditProfileVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public IFormFile UploadFile { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }

    }
}
