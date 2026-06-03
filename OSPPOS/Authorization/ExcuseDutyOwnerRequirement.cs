using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DMX.Authorization
{
    public class ExcuseDutyOwnerRequirement : IAuthorizationRequirement
    {
        public ExcuseDutyOwnerRequirement()
        {
            // Add any properties needed for the requirement if necessary
        }
    }

}