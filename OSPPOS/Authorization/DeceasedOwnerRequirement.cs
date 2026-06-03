using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DMX.Authorization
{
    public class DeceasedOwnerRequirement : IAuthorizationRequirement
    {
        public DeceasedOwnerRequirement()
        {
            // Add any properties needed for the requirement if necessary
        }
    }

}