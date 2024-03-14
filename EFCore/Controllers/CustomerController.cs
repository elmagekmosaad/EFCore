using EFCore.Data.Models;
using EFCore.MySQL.Models.Dto;
using EFCore.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore.Models.Interfaces;
using AutoMapper;
using EFCore.Models.Repository;
using EFCore.Models.Dtos;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await customerRepository.GetAll();
            var data = mapper.Map<IEnumerable<CustomerDto>>(customers);

            return Ok(data);
        }
        [HttpGet("GetAllWithSubscriptions")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomersWithSubscriptions()
        {
            var customers = await customerRepository.GetAllCustomersWithSubscriptions();
            var entities = mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(entities);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetByGender(Gender gender)
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
                var entity = mapper.Map<Customer>(customerDto);
                await customerRepository.Add(entity);

                return Ok("Customer added successfully");
            }
            else
            {
                return BadRequest("Invalid model state");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Read(int id)
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
        public async Task<IActionResult> Delete(int id)
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
