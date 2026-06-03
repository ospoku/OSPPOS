using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class AddFeeStructureVM
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public int DeceasedTypeId{get;set;}
        public SelectList DeceasedTypes { get; set; }
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 0;   
        
        public decimal Fee { get; set; } = 0;
    }
}
