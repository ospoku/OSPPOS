using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class DocumentCommentVM
    {
        public Guid MemoId { get; set; }
        public string MemoContent { get; set; }
        public string Title { get; set; }
        public byte[] Document { get; set; }
        public int CommentCount { get; set; }
        public string NewComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<LetterComment> Comments { get; set; }  
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
