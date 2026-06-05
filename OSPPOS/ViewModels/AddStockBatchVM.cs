using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class AddStockBatchVM
    {
        [Required] public int SupplierId { get; set; }
        [Required] public DateTime ReceivedDate { get; set; } = DateTime.Today;
        public string? SupplierInvoiceRef { get; set; }
        public string? Notes { get; set; }
        public List<StockBatchItemVm> Items { get; set; } = [new()];
    }
}
