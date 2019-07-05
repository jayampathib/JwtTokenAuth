using FileServer.Model;
using JwtDomain;
using JwtDomain.Repo;
using JwtWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
            List<AppUserClaimVm> claims = new List<AppUserClaimVm>();

            // Set User Properties
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            //ret.BearerToken = new Guid().ToString();

            // Get all claims for this user
            //claims = authUser.AppUserClaims.ToList().Select(x => new AppUserClaimVm()
            //{ AppUserId = x.AppUserId, ClaimType = x.ClaimType, ClaimValue = x.ClaimValue }).ToList();

            // Loop through all claims and 
            // set properties of user object
            /*foreach (AppUserClaim claim in claims)
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
            }*/
            ret.BearerToken =BuildJwtToken(ret, authUser.AppUserClaims.ToList());
            return ret;
        }


        protected string BuildJwtToken(AppUserAuthVM authUser,List<AppUserClaim> appUserClaims)
        {
            // symetric key read from appsettings
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_settings.Key));

            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // Add custom claims
            jwtClaims.Add(new Claim("isAuthenticated", authUser.IsAuthenticated.ToString().ToLower()));
            foreach (var item in appUserClaims)
            {
                jwtClaims.Add(new Claim(item.ClaimType, item.ClaimValue.ToString().ToLower()));
            }
            //jwtClaims.Add(new Claim("canAccessProducts", authUser.CanAccessProducts.ToString().ToLower()));
            //jwtClaims.Add(new Claim("canAddProduct", authUser.CanAddProduct.ToString().ToLower()));
            //jwtClaims.Add(new Claim("canSaveProduct", authUser.CanSaveProduct.ToString().ToLower()));
            //jwtClaims.Add(new Claim("canAccessCategories", authUser.CanAccessCategories.ToString().ToLower()));
            //jwtClaims.Add(new Claim("canAddCategory", authUser.CanAddCategory.ToString().ToLower()));

            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: jwtClaims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
 
