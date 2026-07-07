namespace OSPPOS.DTO.Product
{
    public class ViewProductsDTO
    {
       public string CategoryId { get; set; }
  
     
        public string Description { get; set; }
        public string ProductCategoryCategoryId { get;    set; }
        
              public int CurrentStock { get; set; } 
                
        public decimal SellingPrice { get; set; }
                  public string IsActive {  get; set; }
                  public string SKU {  get; set; }
                public int SupplierId { get; set; }
               public string UnitId {  get; set; }
                 public string CostPrice {  get; set; }
            public string ReorderLevel { get; set; }
             public string   Name
        { get; set; }
        public string WholesalePrice { get; set; }
    }
}
