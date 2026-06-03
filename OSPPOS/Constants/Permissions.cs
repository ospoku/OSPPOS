using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMX.Constants
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Comment",
                $"Permissions.{module}.Download",
                $"Permissions.{module}.Print",
            };
        }

        public static class Modules
        {
            public const string ViewLetters = "Permissions.Modules.ViewLetters";
            public const string CreateLetter = "Permissions.Modules.CreateLetter";
            public const string EditLetter = "Permissions.Modules.EditLetter";
            public const string DeleteLetter= "Permissions.Modules.DeleteLetter";
            public const string CommentLetter = "Permissions.Modules.CommentLetter";
            public const string DownloadLetter = "Permissions.Modules.DownloadLetter";
            public const string ViewMemo = "Permissions.Modules.ViewMemo";
            public const string CreateMemo = "Permissions.Modules.CreateMemo";
            public const string EditMemo = "Permissions.Modules.EditMemo";
            public const string CommentMemo = "Permissions.Modules.CommentMemo";
            public const string DeleteMemo = "Permissions.Modules.DeleteMemo";
            public const string DeleteSickReport = "Permissions.Modules.DeleteSickReport";
            public const string EditSickReport= "Permissions.Modules.EditSickReport";
            public const string CommentSickReport = "Permissions.Modules.CommentSickReport";
  
            public const string CommentTravelRequest = "Permissions.Modules.CommentTravelRequest";
            public const string CommentServiceRequest = "Permissions.Modules.CommentServiceRequest";
            public const string CommentDeceased = "Permissions.Modules.CommentDeceased";
            public const string CommentPettyCash = "Permissions.Modules.CommentPettyCash";
            public const string CommentExcuseDuty = "Permissions.Modules.CommentExcuseDuty";
            public const string EditTravelRequest = "Permissions.Modules.EditTravelRequest";
            public const string EditServiceRequest = "Permissions.Modules.EditServiceRequest";
            public const string EditDeceased = "Permissions.Modules.EditDeceased";
            public const string EditPettyCash = "Permissions.Modules.EditPettyCash";
            public const string EditExcuseDuty = "Permissions.Modules.EditExcuseDuty";
            public const string CreateTravelRequest = "Permissions.Modules.CreateTravelRequest";
            public const string CreateServiceRequest = "Permissions.Modules.CreateServiceRequest";
            public const string CreateDeceased = "Permissions.Modules.CreateDeceased";
            public const string CreatePettyCash = "Permissions.Modules.CreatePettyCash";
            public const string CreateExcuseDuty = "Permissions.Modules.CreateExcuseDuty";
            public const string DeleteTravelRequest = "Permissions.Modules.DeleteTravelRequest";
            public const string DeleteServiceRequest = "Permissions.Modules.DeleteServiceRequest";
            public const string DeleteDeceased = "Permissions.Modules.DeleteDeceased";
            public const string DeletePettyCash = "Permissions.Modules.DeletePettyCash";
            public const string DeleteExcuseDuty = "Permissions.Modules.DeleteExcuseDuty";
            public const string PrintTravelRequest = "Permissions.Modules.PrintTravelRequest";
            public const string PrintServiceRequest = "Permissions.Modules.PrintServiceRequest";
            public const string PrintDeceased = "Permissions.Modules.PrintDeceased";
            public const string PrintPettyCash = "Permissions.Modules.PrintPettyCash";
            public const string PrintExcuseDuty = "Permissions.Modules.PrintExcuseDuty";
            public const string PrintSickReport = "Permissions.Modules.PrintSickReport";
        }
        public static class User
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string ManageRoles = "Permissions.Users.ManageRoles";
            public const string ManageClaims = "Permissions.Users.ManageClaims";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string ManageClaims = "Permissions.Roles.ManageClaims";

        }
    }
}