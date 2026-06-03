using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class AddMemoVM
    {
        [Required]
        public string Content { get; set; }

        public List<string> SelectedUsers { get; set; }
        [Required(ErrorMessage = "Please select assignees")]

        public SelectList UsersList { get; set; }
        [Required]
        public string Title { get; set; }
        public string Receipient { get; set; }
        public string Sender { get; set; }
    }
}
