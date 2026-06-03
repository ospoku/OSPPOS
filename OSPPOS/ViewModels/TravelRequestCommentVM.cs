using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class TravelRequestCommentVM
    {
        public string TravelRequestId { get; set; }
        public string MemoContent { get; set; }
        public string Title { get; set; }
        public int CommentCount { get; set; }
        public string NewComment { get; set; }
        public ICollection<TravelRequestComment> Comments { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public decimal FuelClaim { get; set; }

        public SelectList TravelTypes { get; set; }
        public string TravelTypeId { get; set; }

        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset StartDate { get; set; }
        

        public SelectList TransportModes { get; set; }
        public string TransportModeId { get; set; }
        public string Purpose { get; set; }
        public int ConferenceFee { get; set; }
       
        public int OtherExpenses { get; set; }
        public string AdditionalNotes { get; set; }


    }
}