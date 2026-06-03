using DMX.Models;

namespace DMX.ViewModels
{
    public class PrintDocumentVM
    {
        public string ReferenceNumber { get; set; }
        public string DocumentSource { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime DocumentDate { get; set; }
        //[FileExtensions(Extensions = "jpg,jpeg,png,pdf")]
        //public IFormFile UploadFile { get; set; }
        public string AdditionalNotes { get; set; }
        //public List<string> SelectedUsers { get; set; }
        //public SelectList UsersList { get; set; }
       public ICollection<LetterComment>Comments { get; set; }  
    }
}
