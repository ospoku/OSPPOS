namespace OSPPOS.ViewModels
{
    public class ViewCustomerSettingsVM
    {
       
            public int CustomerId { get; set; }
            public string Name { get; set; } = null!;
            public bool IsActive { get; set; }
            public bool AllowCredit { get; set; }
            public decimal CreditLimit { get; set; }
        }
    
}

