using Web.Api.Authorization.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Api.Constants;

namespace EFCore.Controllers
{
    public class TestController : ControllerBase
    {
        //This resource is For all types of role
        //[Authorize(Roles = "SuperAdmin, Admin, Customer")]
        //[Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Customer}")]
        [AuthorizeOnAnyOnePolicy($"{Roles.SuperAdmin},{Roles.Admin},{Roles.Customer}")]
        [HttpGet]
        [Route("api/test/resource1")]
        public IActionResult GetResource1()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello: " + identity.Name);
        }
        //This resource is only For Admin and SuperAdmin role
        //[Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        [AuthorizeOnAnyOnePolicy($"{Policies.SuperAdmin}, {Policies.Admin}")]

        [HttpGet]
        [Route("api/test/resource2")]
        public IActionResult GetResource2()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);

            return Ok("resource2 Hello " + identity.Name + " Your Roles(s) are: " + string.Join(",", roles.ToList()));
        }
        //This resource is only For SuperAdmin role
        //[Authorize(Roles = Roles.SuperAdmin)]
        [AuthorizeOnAnyOnePolicy(Policies.SuperAdmin)]
        [HttpGet]
        [Route("api/test/resource3")]
        public IActionResult GetResource3()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("resource3 Hello " + identity.Name + " Your Roles(s) are: " + string.Join(",", roles.ToList()));
        }
        [HttpGet]
        [Route("api/mvc")]
        [AllowAnonymous]
        public IActionResult testMvc()
        {
            return Ok("testMvc Hello mvc");
        }
    }
}
