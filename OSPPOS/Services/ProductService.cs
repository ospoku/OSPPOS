using DocumentFormat.OpenXml.ExtendedProperties;
using OSPPOS.Data;
using OSPPOS.DTO;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System.Security.Claims;
using static DMX.Constants.Permissions;

namespace OSPPOS.Services
{
    public class ProductService(XContext ctx, EntityService entityService) : IProductService
    {



        public async Task<(bool Success, string Error, Product? Product)> AddProductAsync(AddProductDTO addProductDTO, ClaimsPrincipal user)
        {
            try
            {

                Product addThisProduct = new() 
                { CategoryId = addProductDTO.CategoryId,
                CurrentStock=addProductDTO.CurrentStock,
                    Description=addProductDTO.Description,
                SellingPrice=addProductDTO.SellingPrice,
                IsActive=addProductDTO.IsActive,
                    SKU=addProductDTO.SKU,
                SupplierId=addProductDTO.SupplierId,
                UnitId=addProductDTO.UnitId,
                CostPrice=addProductDTO.CostPrice,
                ReorderLevel=addProductDTO.ReorderLevel,
                Name=addProductDTO.Name,};
             
     


                bool result = await entityService.AddEntityAsync(addThisProduct, user);

                if (!result)
                {
                    return (false, "Error!", null);
                }
                else
                {


                    return (true, "", null);
                }
            }
            catch (Exception ex)
            {
                var fullError = ex.ToString(); // includes stack trace
                return (false, fullError, null);
            }

          
        }



        public Task<string> GenerateOrderNumberAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SaleOrder?> GetOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SaleOrder>> GetOrdersAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Error)> RecordPaymentAsync(RecordPaymentVM vm, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
