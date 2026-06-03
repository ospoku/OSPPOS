using DMX.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    [RequireAntiforgeryToken]
    public class AddDeceasedVM
    {
        public string DeceasedId { get; set; }
        [Required]
  
        public  string ReferenceNo { get; set; }
        [Required]
        public string DeceasedName { get; set; }
        [Required]
        public string Depositor { get; set; }
        [Required]
        public string DepositorAddress { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Diagnoses { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z\s]+")]

        public string WardInCharge { get; set; }
        public DateTime? Date { get; set; }
        public string TagNo { get; set; }
        public string FolderNo {  get; set; }
        public int DeceasedTypeId { get; set; }
        public SelectList DeceasedTypes { get; set; }
        public string Description { get; set; }
        public  List<CheckBoxItem>MorgueServices { get; set; }
        public List<string> SelectedUsers { get; set; }
  
        public SelectList UsersList { get; set; }
       
    }
}
