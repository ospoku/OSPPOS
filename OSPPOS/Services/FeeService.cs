using DMX.Data;
using DMX.Models;
using System;
using System.Linq;

namespace DMX.Services
{
    public class FeeService
    {
        private readonly XContext _context;

        public FeeService(XContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Calculates the fee based on the number of days and the deceased type (e.g., died in ward or brought in dead).
        /// </summary>
        /// <param name="numberOfDays">The number of days the deceased stayed.</param>
        /// <param name="deceasedTypeId">The ID of the deceased type (e.g., 1 = died in ward, 2 = brought in dead).</param>
        /// <returns>The calculated fee.</returns>
        public decimal FeeCalculator(int numberOfDays, int deceasedTypeId, List<DeceasedService> selectedServices)
        {
            decimal totalFee = 0;

           // Fetch the fee structures based on the deceased type ID
            var feeStructures = _context.FeeStructures
                .Where(f => f.DeceasedTypeId == deceasedTypeId) // Use the foreign key to match fee structures
                .OrderBy(f => f.MinDays)
                .ToList();

            if (!feeStructures.Any())
            {
                throw new InvalidOperationException("No fee structures found for the specified deceased type.");
            }

            foreach (var tier in feeStructures)
            {
                if (numberOfDays > tier.MaxDays)
                {
                   // Calculate fee for the entire tier

                   totalFee += (tier.MaxDays - tier.MinDays + 1) * tier.Fee;
                }
                else if (numberOfDays >= tier.MinDays)
                {
                   // Calculate fee for the remaining days within the tier

                   totalFee += (numberOfDays - tier.MinDays + 1) * tier.Fee;
                    return totalFee;
                }
            }

            //Handle days beyond the last tier
            var lastTier = feeStructures.LastOrDefault();
            if (lastTier != null && numberOfDays > lastTier.MaxDays)
            {
                totalFee += (numberOfDays - lastTier.MaxDays) * lastTier.Fee;
            }


            //Add charges for selected extra services
            if (selectedServices != null && selectedServices.Any())
                {
                    foreach (var service in selectedServices)
                    {
                        totalFee += service.MorgueService.Amount;
                    }
                }


            return totalFee;
        }




    }
}