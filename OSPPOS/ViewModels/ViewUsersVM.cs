using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class ViewUsersVM
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Telephone { get; set; }
      
        public string Role { get; set; }
    }
}
