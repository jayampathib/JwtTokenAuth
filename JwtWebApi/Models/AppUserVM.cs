using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtWebApi.Models
{
    public class AppUserVM
    {
         
         
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(30)]
        [Required]
        public string Password { get; set; }

         
    }
}
