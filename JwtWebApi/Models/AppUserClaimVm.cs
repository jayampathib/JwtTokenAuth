using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtWebApi.Models
{
    public class AppUserClaimVm
    {
        public Guid AppUserId { get; set; }     
 
        public string ClaimType { get; set; }
 
        public string ClaimValue { get; set; }
    }
}
