using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class AddCustomerVM
    {
        public string PublicId { get; set; }
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public decimal CreditLimit { get; set; } = 0;
        public bool AllowCredit { get; set; } = false;
        public bool IsActive { get; set; } = true;


    }
}

