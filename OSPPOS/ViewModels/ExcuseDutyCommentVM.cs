using CsvHelper.Configuration.Attributes;
using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class ExcuseDutyCommentVM
    {
        public string NewComment { get; set; }
        public ICollection<ExcuseDutyComment> Comments { get; set; }
        public string Diagnosis { get; set; }
        public string PatientId { get;  set; }
        public string PatientName { get; set; }
        
        public DateTime DischargeDate { get; set; }
        public int Days { get; set; }
        public int CommentCount { get; set; }
        public SelectList UsersList { get;  set; }
        public List<string> SelectedUsers { get; set; }
    }
}
