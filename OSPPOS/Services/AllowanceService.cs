using DMX.Data;
using DMX.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace DMX.Services
{

    public class AllowanceService
    {
        private readonly XContext _context;

        public AllowanceService(XContext context)
        {
            _context = context;
        }
        public decimal PerdiemCalculator(int noOfDays, string userId)
        {
            // Validate number of days
            if (noOfDays < 0)
            {
                throw new ArgumentException("Number of days cannot be negative.", nameof(noOfDays));
            }

            // Validate userId
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            // Fetch the per diem amount for the user
            var perDiem = _context.PerDiems
                .FirstOrDefault(p => p.AppUser.Id == userId); // Use FirstOrDefault for better clarity

            // Handle case where per diem is not found
            if (perDiem == null)
            {
                throw new ArgumentException($"Per diem not found for user ID: {userId}", nameof(userId));
            }

            // Calculate and return the total per diem amount
          
        decimal amount = perDiem.Amount;
    if (amount <= 0)
    {
        // Log a warning (optional)
     

        // Use a default value (e.g., 50)
        amount = 0; // Replace with your default value or fallback logic
    }

    // Calculate and return the total per diem amount
    return noOfDays* amount;
}
        public decimal TotalAllowance(decimal conferenceFee, decimal fuelClaim,int noOfDays, string userId)
        {
            decimal totalAllowance = 0;
             totalAllowance =conferenceFee+ fuelClaim+ PerdiemCalculator(noOfDays,userId);
            return totalAllowance;
        }
        
    }
}
