using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class LoginVM
    {
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
