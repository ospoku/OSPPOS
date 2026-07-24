
using OSPPOS.DTO.Patient;
using OSPPOS.DTO.Product;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Interfaces
{
    public interface IPatientService
    {
        Task<(bool Success, string Error, Patient? Patient)> AddPatientAsync(AddPatientDTO addPatientDTO, ClaimsPrincipal user);
        Task<List<ViewPatientsDTO>> ViewPatientsAsync(ViewPatientsDTO viewPatientsDTO);
   
   
    }
}
