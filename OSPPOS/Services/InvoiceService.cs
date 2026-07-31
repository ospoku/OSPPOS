using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.DTO.Customer;
using OSPPOS.DTO.Invoice;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System.Security.Claims;
using static DMX.Constants.Permissions;

namespace OSPPOS.Services
{
    public class InvoiceService(XContext ctx, EntityService entityService) : IInvoiceService
    {
    //    public async Task<(bool Success, string Error, Invoice? Invoice)> AddInvoiceAsync(AddInvoiceDTO addInvoiceDTO, ClaimsPrincipal user)
    //    {
    //        try
    //        {
    //            Invoice addThisInvoice = new()
    //            {
    //                CustomerId = addInvoiceDTO.CustomerId,
    //                Discount = addInvoiceDTO.Discount,
    //                DueDate = addInvoiceDTO.DueDate,
    //                Notes = addInvoiceDTO.Notes,
    //                InvoiceNumber = addInvoiceDTO.InvoiceNumber,
    //                InvoiceDate = addInvoiceDTO.InvoiceDate,
    //                WalkInCustomerName = addInvoiceDTO.WalkInCustomer
    //};
    //            bool result = await entityService.AddEntityAsync(addThisInvoice, user);
    //            if (!result)
    //            {
    //                return (false, "Error!", null);
    //            }
    //            else
    //            {
    //                return (true, "", addThisInvoice);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            var fullError = ex.ToString(); // includes stack trace
    //            return (false, fullError, null);
    //        }  
    //    }

    //    public async Task<(bool Success, string Error, Invoice? Invoice)> AddInvoiceAsync(
    //AddInvoiceDTO addInvoiceDTO,
    //ClaimsPrincipal user)
    //    {
    //        using var transaction = await ctx.Database.BeginTransactionAsync();

    //        try
    //        {


    //            Invoice addThisInvoice = new()
    //            {
    //                CustomerId = addInvoiceDTO.CustomerId,
    //                Discount = addInvoiceDTO.Discount,
    //                DueDate = addInvoiceDTO.DueDate,
    //                Notes = addInvoiceDTO.Notes,

    //                InvoiceDate = DateTime.UtcNow,
    //                WalkInCustomerName = addInvoiceDTO.WalkInCustomer,
    //                InvoiceNumber = addInvoiceDTO.InvoiceNumber
    //            };
    //            bool result = await entityService.AddEntityAsync(addThisInvoice, user);
    //            if (!result)
    //            {
    //                return (false, "Error!", null);
    //            }
    //            else
    //            {
    //                return (true, "", addThisInvoice);
    //            }

    //            // 🔥 HANDLE ITEMS + STOCK
    //            foreach (var item in addInvoiceDTO.Items)
    //            {
    //                var product = await ctx.Products.FindAsync(item.ProductId);

    //                if (product == null)
    //                    throw new Exception($"Product {item.ProductId} not found");

    //                if (product.CurrentStock < item.Quantity)
    //                    throw new Exception($"Not enough stock for {product.Name}");

    //                // Reduce stock
    //                product.CurrentStock -= item.Quantity;

    //                // Add invoice item
    //                ctx.InvoiceItems.Add(new InvoiceItem
    //                {
    //                    InvoiceId = invoice.Id,
    //                    ProductId = item.ProductId,
    //                    Quantity = item.Quantity,
    //                    UnitPrice = product.CostPrice
    //                });



    //                await transaction.CommitAsync();

    //                return (true, "", invoice);
    //            }
    //        }

    //        catch (Exception ex)
    //        {
    //            await transaction.RollbackAsync();
    //            return (false, ex.ToString(), null);
    //        }
    //    }

        public async Task<(bool Success, string Error, Invoice? Invoice)> AddInvoiceAsync(
    AddInvoiceDTO addInvoiceDTO,
    ClaimsPrincipal user)
        {
            using var transaction = await ctx.Database.BeginTransactionAsync();

            //try
            //{
                Invoice addThisInvoice = new()
                {
                    CustomerId = addInvoiceDTO.CustomerId,
                    Discount = addInvoiceDTO.Discount,
                    DueDate = addInvoiceDTO.DueDate,
                    Notes = addInvoiceDTO.Notes,
                    InvoiceDate = DateTime.UtcNow,
                    WalkInCustomerName = addInvoiceDTO.WalkInCustomer,
                    InvoiceNumber = addInvoiceDTO.InvoiceNumber
                };

       

                // 🔥 HANDLE ITEMS + STOCK
                foreach (var item in addInvoiceDTO.Items)
                {
                    var product = await ctx.Products.FindAsync(item.ProductId);

                    if (product == null)
                        throw new Exception($"Product {item.ProductId} not found");

                    if (product.CurrentStock < item.Quantity)
                        throw new Exception($"Not enough stock for {product.Name}");

                    // Reduce stock
                    product.CurrentStock -= item.Quantity;

                    // Add invoice item
                    addThisInvoice.Items.Add(new InvoiceItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.CostPrice,
                        
                    });
                }

           await entityService.SaveAsync(user);

       

            await transaction.CommitAsync();

                return (true, "", addThisInvoice);
            }
            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();
            //    return (false, ex.Message, null);
            //}
        //}
        public Task<List<SaleOrder>> GetOrdersAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Error)> RecordPaymentAsync(RecordPaymentVM vm, string userId)
        {
            throw new NotImplementedException();
        }

      public async  Task<List<ViewInvoicesDTO>> ViewInvoicesAsync(ViewInvoicesDTO viewInvoicesDTO)

        {
            var invoices= ctx.Invoices.Select(i => new ViewInvoicesDTO() 
            {
              DueDate = i.DueDate,
              InvoiceDate = i.InvoiceDate,
              CustomerId=i.CustomerId,
                Discount=i.Discount,
                InvoiceNumber=i.InvoiceNumber
            }).ToListAsync();

            return await invoices;
        }

     public   Task<Invoice?> GetInvoiceAsync(int id)
        {
            return  ctx.Invoices.FirstOrDefaultAsync(i => i.Id == id);
        }

     public   Task<List<Invoice>> GetInvoicesAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInvoiceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateInvoiceAsync(UpdateInvoiceDTO dto, ClaimsPrincipal user)
        {

            try
            {
                var invoice = await ctx.Invoices.FindAsync(dto);
                if (invoice == null)
                {
                    return (false);
                }
                // map updates here
               dto.DueDate=invoice.DueDate;
                dto.ModifiedDate=invoice.ModifiedDate;
                dto.ModifiedBy=invoice.ModifiedBy;
                dto.Discount = invoice.Discount;

                await entityService.EditEntityAsync(invoice,user);

                return (true);
            }
            catch
            {
                return (false);
            }
        }

        public Task<string?> GetInvoiceForEditAsync(int id)
        {
            throw new NotImplementedException();
        }

  

    }
}
