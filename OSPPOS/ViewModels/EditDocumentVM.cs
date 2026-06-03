using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class EditLetterVM
    {

        public string ReferenceNumber { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DocumentDate { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg,png,pdf")]
        public IFormFile UploadFile { get; set; }
        public string AdditionalNotes { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public string Id { get; set; }


    }
}
