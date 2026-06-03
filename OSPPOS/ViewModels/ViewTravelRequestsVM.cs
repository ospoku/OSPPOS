namespace DMX.ViewModels
{
    public class ViewTravelRequestsVM
    {
        public string TravelRequestId { get; set; }

        public string ReferenceNumber { get; set; }
        public string Name { get; set; }
        public string RankId { get; set; }
        public string DepartmentId { get; set; }
        public decimal ConferenceFee { get; set; }
        public DateTimeOffset DepartureDate { get; set; }
        public string TravelType { get; set; }
        public decimal TransportExpenses { get; set; }
        public int NightAbsent { get; set; }
        public DateTimeOffset EndDate { get; set; }
       
        public DateTimeOffset StartDate { get; set; }
        public decimal TotalAllowance { get; set; } = 0;
        public decimal Rate { get; set; } = 0;
        public decimal FuelClaim { get; set; } = 0;
        public decimal AmountDue { get; set; } = 0;
        public string PurposeofJourney { get; set; }
        public DateTimeOffset? CreatedDate { get;  set; }
        public string? CreatedBy { get; set; }
    }
}
