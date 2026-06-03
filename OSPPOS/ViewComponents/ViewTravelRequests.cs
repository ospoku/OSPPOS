using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMX.Models;
using DMX.Services;
using System;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewComponents
{
    public class ViewTravelRequests(XContext dContext, UserManager<AppUser> userManager, AllowanceService allowance) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly AllowanceService als = allowance;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Get the current user's ID
            var user = (await usm.GetUserAsync(HttpContext.User)).Id;

            // Fetch the travel list for the user
            var travelList = await dcx.TravelRequestAssignments
                .Where(a => a.AppUser.Id == user || a.TravelRequest.CreatedBy == user && !a.TravelRequest.IsDeleted)
                .Select(a => new ViewTravelRequestsVM
                {
                    ReferenceNumber = a.TravelRequest.ReferenceNumber,
                    TravelType = a.TravelRequest.TravelType.Name,
                    Name = usm.FindByIdAsync(a.TravelRequest.CreatedBy).Result.Fullname,
                    DepartureDate = a.TravelRequest.StartDate,
                    TravelRequestId = a.TravelRequestId,
                    PurposeofJourney = a.TravelRequest.Purpose,
                    StartDate = a.TravelRequest.StartDate, // Fixed: Changed from EndDate to StartDate
                    EndDate = a.TravelRequest.EndDate, // Added: EndDate for clarity
                    CreatedBy = a.CreatedBy,
                 
                    CreatedDate = a.CreatedDate,
                    ConferenceFee = a.TravelRequest.ConferenceFee, // Added: ConferenceFee
                    FuelClaim = a.TravelRequest.FuelClaim // Added: FuelClaim
                })
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync(); // Use ToListAsync for async database operations

            // Calculate total allowance for each travel request
            foreach (var travel in travelList)
            {
                // Calculate the number of days between StartDate and EndDate
                var numberOfDays = (int)(travel.EndDate - travel.StartDate).TotalDays;

                // Calculate total allowance using the TotalAllowance method
                var totalAllowance = als.TotalAllowance(travel.ConferenceFee, travel.FuelClaim, numberOfDays, travel.CreatedBy);

                // Assign the calculated total allowance to the travel object
                travel.TotalAllowance = totalAllowance;
            }

            // Return the view with the travel list
            return View(travelList);
        }
    }
}