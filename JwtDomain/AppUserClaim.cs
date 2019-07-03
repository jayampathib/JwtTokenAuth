using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwtDomain
{
    public class AppUserClaim
    {
        [Key]
        public Guid AppUserClaimId { get; set; }
        [Required]
        public Guid AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClaimType { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClaimValue { get; set; }
    }
}
