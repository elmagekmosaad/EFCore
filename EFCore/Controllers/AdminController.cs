using Web.Api.Authorization.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Constants;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeOnAnyOnePolicy($"{Policies.SuperAdmin}, {Policies.Admin}")]
    public class AdminController : ControllerBase
    {
    }
}
