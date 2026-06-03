using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class ViewPerDiemsVM
    {
        public string Id { get; set; }
        public string Staff { get; set; }
        public string Department { get; set; }
        public string Rank { get; set; }
        public decimal Amount { get; set; }
    }
}
