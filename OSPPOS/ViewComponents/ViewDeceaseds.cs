using DMX.Data;
using DMX.Models;
using DMX.Services;
using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMX.ViewComponents
{
    public class ViewDeceaseds : ViewComponent
    {
        private readonly XContext _context;
        private readonly FeeService _feeService;

        public ViewDeceaseds(XContext context, FeeService feeService)
        {
            _context = context;
            _feeService = feeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {

                // Fetch deceased records
                var deceasedRecords = await _context.Deceased
                    .Where(a => !a.IsDeleted)
                    .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();

                // Fetch all services for the deceased records in a single query
                var deceasedIds = deceasedRecords.Select(a => a.PublicId).ToList();
                var allServices = await _context.DeceasedServices
                    .Include(d => d.MorgueService)
                    .Where(d => deceasedIds.Contains(d.PublicId))
                    .ToListAsync();

                // Group services by DeceasedId for easy lookup
                var servicesByDeceasedId = allServices
                    .GroupBy(d => d.PublicId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Process deceased records
                var deceasedList = deceasedRecords.Select(deceased =>
                {
                    TimeSpan timeSpan = DateTime.Now - deceased.CreatedDate.Value;
                    int numberOfDays = (int)timeSpan.TotalDays;

                    // Get the services for the current deceased record
                    var selectedServices = servicesByDeceasedId.ContainsKey(deceased.PublicId)
                        ? servicesByDeceasedId[deceased.PublicId]
                        : new List<DeceasedService>();

                    // Calculate the total amount of selected services
                    decimal totalServiceAmount = selectedServices.Sum(service => service.MorgueService.Amount);

                    // Calculate fees
                    decimal fees = _feeService.FeeCalculator(numberOfDays, deceased.DeceasedTypeId, selectedServices) + totalServiceAmount;

                    return new ViewPatientsVM
                    {
                        PatientId = deceased.PublicId,
                        PatientName = deceased.Name,
                        FinalDiagnoses = deceased.Diagnoses,
                        FolderNo = deceased.FolderNo,
                        WardInCharge = deceased.WardInCharge,
                        OtherFees = fees,
                        TagNo = deceased.TagNo,
                        CreatedDate = deceased.CreatedDate
                    };
                }).ToList();

                return View(deceasedList);
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                // Return an empty list or an error view
                return View(new List<ViewPatientsVM>());
            }
        }
    }
}