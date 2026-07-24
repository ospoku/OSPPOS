using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;

using OSPPOS.DTO.Patient;
using OSPPOS.DTO.Product;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System.Security.Claims;
using static DMX.Constants.Permissions;

namespace OSPPOS.Services
{
    public class PatientService(XContext ctx, EntityService entityService) : IPatientService
    {
        public async Task<(bool Success, string Error, Patient? Patient)> AddPatientAsync(AddPatientDTO addPatientDTO, ClaimsPrincipal user)
        {
            try
            {

                Patient addThisPatient = new() 
                { };
             
     


                bool result = await entityService.AddEntityAsync(addThisPatient, user);

                if (!result)
                {
                    return (false, "Error!", null);
                }
                else
                {


                    return (true, "", addThisPatient);
                }
            }
            catch (Exception ex)
            {
                var fullError = ex.ToString(); // includes stack trace
                return (false, fullError, null);
            }

          
        }



      

      public async  Task<List<ViewPatientsDTO>> ViewPatientsAsync(ViewPatientsDTO viewPatientsDTO)

        {
            var patients= ctx.Patients.Select(p => new ViewPatientsDTO() 
            {
             
          
            }).ToListAsync();

            return await patients;
        }


        
    }
}
