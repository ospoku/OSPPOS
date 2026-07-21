namespace OSPPOS.DTO.Customer
{
    public class ViewCustomersDTO
    {
       public int CategoryId { get; set; }
        public string? Description { get; set; } = string.Empty;
       
      
              public int CurrentStock { get; set; } 
        public decimal SellingPrice { get; set; }
                  public bool IsActive {  get; set; }
        public string SKU { get; set; } = string.Empty;
                public int SupplierId { get; set; }
               public int UnitId {  get; set; }
                 public decimal CostPrice {  get; set; }
            public int ReorderLevel { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal WholesalePrice { get; set; }
    }
}
