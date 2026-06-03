using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class DetailMemoVM
    {
    
        public string Title { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
        public string Recipient { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }


    }
}
