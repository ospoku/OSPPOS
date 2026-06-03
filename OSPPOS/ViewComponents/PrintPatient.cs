using DMX.Controllers;
using DMX.Data;
using DMX.Services;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DMX.ViewComponents
{
    public class PrintPatient(XContext dContext, FeeService feeService, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly FeeService fs = feeService;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)
        {
           
            var unprotectedId=protector.Unprotect(Id);
            if(!Guid.TryParse(Id, out Guid patientGuid))
            {

            }
            var deceased = dcx.Deceased.Include(d => d.DeceasedComments.OrderBy(d => d.CreatedDate)).Include(d=>d.DeceasedServices).Where(d => d.IsDeleted == false & d.PublicId == patientGuid).Select(d => d)
            .FirstOrDefault();
            TimeSpan difference = DateTime.Now - deceased.CreatedDate.Value;
            int numberOfDays = (int)difference.TotalDays;
           var selectedServices = dcx.DeceasedServices.Where(d=>d.PublicId==patientGuid);
            PrintMorgueVM printMorgueVM = new()
            {
                FinalDiagnoses = deceased.Diagnoses,
                FolderNo = deceased.FolderNo,
                
                DeceasedTypeId = deceased.DeceasedTypeId,
                DepositorAddress = deceased.DepositorAddress,
                Depositor = deceased.Depositor,
                Description = deceased.Description,
                TagNo = deceased.TagNo,
                WardInCharge = deceased.WardInCharge,
                AccruedFees = fs.FeeCalculator(numberOfDays, deceased.DeceasedTypeId, selectedServices.ToList()),
            };
                return View(printMorgueVM);
            }
        }
    }

