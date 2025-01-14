using Web.Api.Authorization.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFCore.Models.Repository;
using EFCore.Models.Repository.Interfaces;
using Web.Api.Constants;

namespace EFCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AuthorizeOnAnyOnePolicy($"{Policies.SuperAdmin}")]
    public class SuperAdminController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        public SuperAdminController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var data = await customerRepository.GetAll();
            return Ok(data);
        }
    }
}
