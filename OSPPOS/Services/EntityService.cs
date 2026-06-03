
using DMX.Data;

using OSPPOS.Helpers;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace OSPPOS.Services
{
    public class EntityService(UserManager<AppUser> userManager, XContext context)
    {
        private readonly UserManager<AppUser> usm = userManager;


        private readonly XContext dcx = context;

        public async Task<bool> AddEntityAsync<T>(T entity, ClaimsPrincipal userClaim) where T : class
        {
            var user = await usm.GetUserAsync(userClaim);
            if (user == null)
            {
                return false;
            }

            entity.GetType().GetProperty("CreatedBy")?.SetValue(entity, user.Id);
            entity.GetType().GetProperty("CreatedDate")?.SetValue(entity, DateTime.UtcNow);

            try
            {
                dcx.Set<T>().Add(entity);
                if (await dcx.SaveChangesAsync(user.Id) > 0)
                {
                   
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public async Task<bool> EditEntityAsync<T>(T model, ClaimsPrincipal userClaim) where T : class
        //{
        //    var user = await usm.GetUserAsync(userClaim);
        //    if (user == null)
        //    {
        //        notyf.Error("User is not authenticated.", 5);
        //        return false;
        //    }

        //    var modelType = model.GetType();
        //    var modifiedByProp = modelType.GetProperty("ModifiedBy");
        //    var modifiedDateProp = modelType.GetProperty("ModifiedDate");

        //    if (modifiedByProp != null)
        //    {
        //        modifiedByProp.SetValue(model, user.UserName);
        //    }

        //    if (modifiedDateProp != null)
        //    {
        //        modifiedDateProp.SetValue(model, DateTime.UtcNow);
        //    }

        //    try
        //    {
        //        dcx.Set<T>().Update(model);

        //        if (await dcx.SaveChangesAsync(user.UserName) > 0)
        //        {
        //            notyf.Success("Record successfully updated!", 5);
        //            return true;
        //        }
        //        else
        //        {
        //            notyf.Error("Error, record could not be updated.", 5);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notyf.Error("An error occurred: " + ex.Message, 5);
        //        return false;
        //    }
        //}
       
        public async Task<bool> DeleteEntityAsync<T>(T model, ClaimsPrincipal userClaim) where T : class
        {
            var user = await usm.GetUserAsync(userClaim);
            if (user == null)
            {
                return false;
            }
            model.GetType().GetProperty("ModifiedBy")?.SetValue(model, user.Id);
            model.GetType().GetProperty("ModifiedDate")?.SetValue(model, DateTime.UtcNow);
            model.GetType().GetProperty("IsDeleted")?.SetValue(model,true);
            try
            {
                dcx.Set<T>().Update(model);

                if (await dcx.SaveChangesAsync(user.Id) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditEntityAsync<T>(T model, ClaimsPrincipal userClaim) where T : class
        {
            var user = await usm.GetUserAsync(userClaim);
            if (user == null)
            {
                return false;
            }

            var modelType = model.GetType();
            var modifiedByProp = modelType.GetProperty("ModifiedBy");
            var modifiedDateProp = modelType.GetProperty("ModifiedDate");

            if (modifiedByProp != null)
            {
                modifiedByProp.SetValue(model, user.Id);
            }

            if (modifiedDateProp != null)
            {
                modifiedDateProp.SetValue(model, DateTime.UtcNow);
            }

            try
            {
                dcx.Set<T>().Update(model);

                if (await dcx.SaveChangesAsync(user.Id) > 0)
                {
                  
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public async Task<bool> EditMemoAsync(string Id, EditMemoVM editMemoVM, ClaimsPrincipal userClaim)
        {
            var unprotectedId = (Id);
            if(!Guid.TryParse(unprotectedId, out Guid memoGuid))
            {
                return false; // Invalid ID format
            }
            var updateThisMemo = await dcx.Memos.FirstOrDefaultAsync(a => a.PublicId == memoGuid);

            if (updateThisMemo == null)
            {
                return false;
            }

            updateThisMemo.Content = editMemoVM.Content;
            updateThisMemo.Title = editMemoVM.Title;

            if (!await EditEntityAsync(updateThisMemo, userClaim))
            {
                return false; // If entity update fails, stop here
            }

            // Remove existing memo assignments
            var existingAssignments = dcx.MemoAssignments.Where(x => x.PublicId  == memoGuid);
            dcx.MemoAssignments.RemoveRange(existingAssignments);

            // Add new memo assignments
            var user = await usm.GetUserAsync(userClaim);
            foreach (var userId in editMemoVM.SelectedUsers)
            {
                dcx.MemoAssignments.Add(new MemoAssignment
                {
                    MemoId = updateThisMemo.Id,
                    UserId = userId,
                    CreatedBy = user?.Id,
                    CreatedDate = DateTime.UtcNow,
                });
            }

            // Save changes after updating assignments
            return await dcx.SaveChangesAsync(user?.Id) > 0;
        }
    }
}

