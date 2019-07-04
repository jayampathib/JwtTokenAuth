using FileServer.Model;
using JwtDomain;
using JwtDomain.Repo;
using JwtWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtWebApi.BLL
{
    public interface ISecurityManager
    {
        AppUserAuthVM ValidateUser(string userName, string passWord);
        AppUserAuthVM BuildUserAuthObject(AppUser authUser);
    }

    public class SecurityManager : ISecurityManager
    {
        private readonly IAppUserRepo _IAppUserRepo;
        private readonly JwtSettings _settings = null;
        public SecurityManager(IAppUserRepo iAppUserRepo, JwtSettings settings)
        {
            _IAppUserRepo = iAppUserRepo;
            _settings = settings;
        }
        public AppUserAuthVM ValidateUser(string userName, string passWord)
        {
            AppUserAuthVM ret = new AppUserAuthVM();

            AppUser authUser = _IAppUserRepo.GetAppUser(userName, passWord);

            if (authUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(authUser);
            }

            return ret;
        }



        public AppUserAuthVM BuildUserAuthObject(AppUser authUser)
        {
            AppUserAuthVM ret = new AppUserAuthVM();
            List<AppUserClaim> claims = new List<AppUserClaim>();

            // Set User Properties
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

            // Get all claims for this user
            claims = authUser.AppUserClaims.ToList();

            // Loop through all claims and 
            // set properties of user object
            foreach (AppUserClaim claim in claims)
            {
                try
                {
                    // TODO: Check data type of ClaimValue
                    typeof(AppUserAuthVM).GetProperty(claim.ClaimType)
                      .SetValue(ret, Convert.ToBoolean(claim.ClaimValue), null);
                }
                catch
                {
                }
            }

            return ret;
        }
    }
}
 
