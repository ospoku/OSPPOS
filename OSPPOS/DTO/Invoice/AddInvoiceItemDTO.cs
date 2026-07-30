namespace OSPPOS.DTO.Invoice
{
    public class AddInvoiceItemDTO
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
