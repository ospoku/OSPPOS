using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class MemoCommentVM
    {
        public string MemoId { get; set; }
        public string MemoContent { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
       public int CommentCount { get; set; }
        public string NewComment { get; set; }  
        public DateTime CreatedDate { get; set; }
        public ICollection<MemoComment> Comments { get; set; }
       
        
    }
}
