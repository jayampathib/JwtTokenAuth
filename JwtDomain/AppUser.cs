using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwtDomain
{
    public class AppUser
    {
        [Key]
        public Guid AppUserId { get; set; }
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(30)]
        [Required]
        public string Password { get; set; }

        public virtual IEnumerable<AppUserClaim> AppUserClaims { get; set; }
    }
}
