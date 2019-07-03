using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/<controller>/5
        [HttpGet]
        public string Get()
        {
            return "value";
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]AppUserVM user)
        {
            IActionResult ret   = StatusCode(StatusCodes.Status200OK, user);

            return ret;
        }
    }
}