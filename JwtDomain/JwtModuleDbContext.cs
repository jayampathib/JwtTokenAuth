using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDomain
{

    public class JwtModuleDbContext : DbContext
    {
        public JwtModuleDbContext(DbContextOptions option) : base(option)
        { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserClaim> AppUserClaims { get; set; }
    }
}
