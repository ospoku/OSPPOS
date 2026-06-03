using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class PettyCashCommentVM
    {
        public string PettyCashId { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public int CommentCount { get; set; }
        public string NewComment { get; set; }
       public ICollection<PettyCashComment>Comments { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
