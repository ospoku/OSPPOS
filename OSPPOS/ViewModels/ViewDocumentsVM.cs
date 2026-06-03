using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class ViewDocumentsVM
    {
   

        public string ReferenceNumber { get; set; }
        public string DocumentSource { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime DocumentDate { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg,png,pdf")]
        public IFormFile UploadFile { get; set; }
        public string AdditionalNotes { get; set; }
        public string Assignees { get; set; }
        
        public string[] SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public Guid LetterId { get; set; }
        public DateTime? CreatedDate { get; set; }
     
    }
}
