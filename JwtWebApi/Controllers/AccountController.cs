using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtWebApi.BLL;
using JwtWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ISecurityManager _ISecurityManager;
        public AccountController(ISecurityManager iSecurityManager)
        {
            _ISecurityManager = iSecurityManager;
        }
        // GET api/<controller>/5
        [HttpGet]
        public string Get()
        {
            return "value";
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]AppUserVM user)
        {
            IActionResult ret = null;
            var auth = _ISecurityManager.ValidateUser(user.UserName, user.Password);

            if (auth.IsAuthenticated)
            {
                ret = StatusCode(StatusCodes.Status200OK, auth);
            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound,
                                 "Invalid User Name/Password.");
            }
            return ret;
        }
    }
}