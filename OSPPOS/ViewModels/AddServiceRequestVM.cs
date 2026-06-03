using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class AddServiceRequestVM
    {
        public string Description { get; set; }
        public string RequestNumber { get; set; }
        public string CategoryId { get; set; }
        public DateTime RequestDate { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png,pdf")]
        public IFormFile UploadFile { get; set; }
        public string StatusId { get; set; }
        public string Title { get; set; }
        public SelectList RequestTypes {  get; set; }
        public SelectList Status { get; set; }
        public SelectList Urgency { get; set; }
        public SelectList Categories { get; set; }
        public string PriorityId { get; set; }
       
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public string RequestTypeId { get;  set; }
    }
}
                      
