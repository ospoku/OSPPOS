namespace DMX.Models
{
    
        public class TierCharge
        {
        public int MinDays { get; set; } = 1;
            public int MaxDays { get; set; }
            public int DaysCharged { get; set; }
        public decimal Fee { get; set; } = 0;
        public decimal TotalCharge { get; set; } = 0;
        }

    }

