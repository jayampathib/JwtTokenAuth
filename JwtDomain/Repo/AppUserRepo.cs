using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JwtDomain.Repo
{
    public interface IAppUserRepo
    {
        AppUser GetAppUser(string UserName, string Password);
    }

    public class AppUserRepo : IAppUserRepo
    {
        private readonly JwtModuleDbContext _JwtModuleDbContext;
        public AppUserRepo(JwtModuleDbContext dbContext)
        {
            _JwtModuleDbContext = dbContext;
        }
        public AppUser GetAppUser(string UserName, string Password)
        {
            try
            {
                return _JwtModuleDbContext.
                    AppUsers
                    .Include(a => a.AppUserClaims) //.Select(c => new AppUserClaim() { AppUserClaimId=c.AppUserClaimId, AppUserId=c.AppUserId, ClaimType =c.ClaimType, ClaimValue=c.ClaimValue }))
                    .Where(
                  u => u.UserName.ToLower() == UserName.ToLower()
                  && u.Password == Password).FirstOrDefault()
                  ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            // Attempt to validate user


        }
    }
}