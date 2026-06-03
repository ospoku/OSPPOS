using AspNetCoreHero.ToastNotification.Abstractions;
using DMX.Data;

using DMX.Models;
using DMX.Services;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMX.ViewComponents
{
    public class ViewInvoice
        : ViewComponent
    {
        private readonly XContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IAuthorizationService _authorizationService;
        private readonly EntityService _entityService;
        private readonly AssignmentService _assignmentService;
        public readonly IDataProtector protector;

        public ViewInvoice(
            XContext context,
            UserManager<AppUser> userManager,
            INotyfService notyfService,
            IAuthorizationService authorizationService,
            EntityService entityService,
            AssignmentService assignmentService, IDataProtectionProvider provider)
        {
            _context = context;
            _userManager = userManager;
            _notyfService = notyfService;
            _authorizationService = authorizationService;
            _entityService = entityService;
            _assignmentService = assignmentService;
            protector = provider.CreateProtector("IdProtector");
        }

        public IViewComponentResult Invoke(string Id)
        {
           
            var decryptedId=protector.Unprotect(Id);
            if (!Guid.TryParse(decryptedId, out Guid invoiceGuid))
            {
            }
            var selectedDeceased = _context.Deceased
                .Where(d => d.PublicId == invoiceGuid)
                .FirstOrDefault();

            if (selectedDeceased == null)
            {
                _notyfService.Error("Deceased record not found.");
                return Content("Deceased record not found.");
            }

            // Calculate the number of days
            TimeSpan timeSpan = DateTime.Now - selectedDeceased.CreatedDate.Value;
            int numberOfDays = (int)timeSpan.TotalDays;
            // Fetch selected services (if any)
            var selectedServices = _context.DeceasedServices
                .Where(s => s.PublicId == selectedDeceased.PublicId).Select(s=>s.MorgueService)
                .ToList();
            // Calculate the tier charges
            var tierCharges = FeeCalculator(numberOfDays, selectedDeceased. DeceasedTypeId, selectedServices);

            // Calculate the total invoice amount
            decimal totalInvoiceAmount = tierCharges.Sum(t => t.TotalCharge) + selectedServices.Sum(s => s.Amount);

            // Populate the ViewModel
            var viewModel = new ViewInvoiceVM
            {
                DeceasedName = selectedDeceased.Name, // Assuming the Deceased model has a Name property
                CreatedDate = selectedDeceased.CreatedDate.Value,
                TotalDays = numberOfDays,
                TotalInvoiceAmount = totalInvoiceAmount,
                TierCharges = tierCharges,
                SelectedServices = selectedServices, // Pass selected services to the view
                BaseFee=tierCharges.Sum(t=>t.TotalCharge),
                ExtraServicesFee=selectedServices.Sum(s=>s.Amount),
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }


        public List<TierCharge> FeeCalculator(int numberOfDays, int deceasedTypeId, List<MorgueService> selectedServices)
        {
            var tierCharges = new List<TierCharge>();

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
                    var daysCharged = tier.MaxDays - tier.MinDays + 1;
                    var totalCharge = daysCharged * tier.Fee;

                    tierCharges.Add(new TierCharge
                    {
                        MinDays = tier.MinDays,
                        MaxDays = tier.MaxDays,
                        DaysCharged = daysCharged,
                        Fee = tier.Fee,
                        TotalCharge = totalCharge
                    });

                    numberOfDays -= daysCharged;
                }
                else if (numberOfDays >= tier.MinDays)
                {
                    // Calculate fee for the remaining days within the tier
                    var daysCharged = numberOfDays - tier.MinDays + 1;
                    var totalCharge = daysCharged * tier.Fee;

                    tierCharges.Add(new TierCharge
                    {
                        MinDays = tier.MinDays,
                        MaxDays = tier.MaxDays,
                        DaysCharged = daysCharged,
                        Fee = tier.Fee,
                        TotalCharge = totalCharge
                    });

                    return tierCharges;
                }
            }

            // Handle days beyond the last tier
            var lastTier = feeStructures.LastOrDefault();
            if (lastTier != null && numberOfDays > 0)
            {
                var daysCharged = numberOfDays; // Charge for the remaining days
                var totalCharge = daysCharged * lastTier.Fee;

                tierCharges.Add(new TierCharge
                {
                    MinDays = lastTier.MinDays,
                    MaxDays = lastTier.MaxDays,
                    DaysCharged = daysCharged,
                    Fee = lastTier.Fee,
                    TotalCharge = totalCharge
                });
            }
            if (selectedServices != null && selectedServices.Any())
            {
                foreach (var service in selectedServices)
                {
                    var totalfee = service.Amount;
                }
            }


            return tierCharges;
        }
    }
}
  
        