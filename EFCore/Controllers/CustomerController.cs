using EFCore.Data.Models;
using EFCore.MySQL.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EFCore.Models.Dtos;
using EFCore.Models.Repository.Interfaces;
using Web.Api.Authorization.Filter;
using Web.Api.Constants;
using Web.Api.Data.Entities.Enums;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(policy: Policies.SuperAdmin)]
    //[Authorize(policy: Policies.Admin)]
    //[Authorize(policy: Policies.Customer)]
    [AuthorizeOnAnyOnePolicy($"{Policies.SuperAdmin}, {Policies.Admin}, {Policies.Customer}")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }
        //[Authorize(Roles = "User,Admin")]
        //[Authorize(policy: Policies.Admin, Roles = $"{Roles.Admin},{Roles.Customer}")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await customerRepository.GetAll();
            var data = mapper.Map<IEnumerable<CustomerDto>>(customers);

            return Ok(data);
        }
        //[Authorize(Roles = $"{Roles.Admin},{Roles.Customer}")]
        [HttpGet("GetAllWithSubscriptions")]
        public async Task<ActionResult<IEnumerable<CustomerWithSubscriptionsDto>>> GetAllCustomersWithSubscriptions()
        {
            var customers = await customerRepository.GetAllCustomersWithSubscriptions();
            var entities = mapper.Map<IEnumerable<CustomerWithSubscriptionsDto>>(customers);
            return Ok(entities);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetByGender(CustomerGender gender)
        {
            var customers = await customerRepository.GetByGender(gender);
            var entities = mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(entities);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            if (customerDto is null)
            {
                return BadRequest("Customer is null");
            }

            if (ModelState.IsValid)
            {
                var entity = mapper.Map<AppUser>(customerDto);
                await customerRepository.Add(entity);

                return Ok("Customer added successfully");
            }
            else
            {
                return BadRequest("Invalid model state");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Read(string id)
        {
            var customer = await customerRepository.GetById(id);

            if (customer is null)
            {
                return NotFound($"Customer Id {id} not exists ");
            }
            var entity = mapper.Map<CustomerDto>(customer);
            return Ok(entity);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ReadWithSubscriptions(int id)
        {
            var customer = await customerRepository.ReadWithSubscriptions(id);
            if (customer is null)
            {
                return NotFound($"Customer Id {id} not exists ");
            }

            var entity = mapper.Map<CustomerWithSubscriptionsDto>(customer);
            return Ok(entity);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto customerDto)
        {
            var customerToUpdate = await customerRepository.ReadWithSubscriptions(id);

            if (customerToUpdate is null)
            {
                return NotFound($"Customer with Id {id} not exists ");
            }

            if (customerDto is null)
            {
                return BadRequest("Customer is null");
            }

            ////customerToUpdate.Subscriptions = customerDto.Subscriptions;
            //if(customerDto.Subscriptions is null)
            //{
            //    customerToUpdate.Subscriptions.Clear();
            //}
            //else
            //{
            //    var updatedSubscriptions = customerDto.Subscriptions.Select(s => s.Id);
            //    var removedSubscriptions = customerToUpdate.Subscriptions.Where(s => !updatedSubscriptions.Contains(s.Id)).ToList();
            //    foreach (var subscription in removedSubscriptions)
            //    {
            //        customerToUpdate.Subscriptions.Remove(subscription);
            //    }
            //    customerRepository.SaveChanges();
            //}
            mapper.Map(customerDto, customerToUpdate);
            await customerRepository.Update(customerToUpdate);

            return Ok("Customer Updated successfully");
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await customerRepository.GetById(id);

            if (customer is null)
            {
                return NotFound($"Customer Id {id} not exists ");
            }

            await customerRepository.Remove(customer);
            return Ok("Customer Removed successfully");
        }
    }
}
