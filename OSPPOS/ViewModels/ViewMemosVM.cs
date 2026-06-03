
using System.Collections.Generic;

namespace DMX.ViewModels
{
    public class ViewMemosVM
    {
        public string Title { get; set; }
        public string Content { get;  set; }

        public string Sender { get; set; }
        public List<string> Assignees { get; set; }
        public string ReferenceNumber { get; set; }
        public string CreatedBy { get; set; }
        public Guid PublicId { get; set; }
        public bool CanPrint { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        public DateTime? CreatedDate { get;  set; }
    }
}
