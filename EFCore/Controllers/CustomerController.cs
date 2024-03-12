using EFCore.Data.Models;
using EFCore.MySQL.Models.DTO;
using EFCore.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore.Models.Interfaces;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await customerRepository.GetAll();

            return Ok(customers);
        }
        [HttpGet("GetAllWithSubscriptions")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomersWithSubscriptions()
        {
            var customers = await customerRepository.GetAllCustomersWithSubscriptions();

            return Ok(customers);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetByGender(Gender gender)
        {
            var customers = await customerRepository.GetByGender(gender);

            return Ok(customers);
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
                try
                {
                    var customer = new Customer
                    {
                        Name = customerDto.Name,
                        Email = customerDto.Email,
                        MobileNumber = customerDto.MobileNumber,
                        Facebook = customerDto.Facebook,
                        Gender = customerDto.Gender,
                        Country = customerDto.Country,
                        Admin = customerDto.Admin,
                        Comments = customerDto.Comments,
                        //Subscriptions = customerDto.Subscriptions
                    };

                   await customerRepository.Add(customer);

                    return Ok("Customer added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest("Invalid model state");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var customer = await customerRepository.GetById(id);

            if (customer is null)
            {
                return NotFound($"Customer Id {id} not exists ");
            }
            CustomerDto customerDto = new()
            {
                Id = id,
                Name = customer.Name,
                Email = customer.Email,
                MobileNumber = customer.MobileNumber,
                Facebook = customer.Facebook,
                Gender = customer.Gender,
                Country = customer.Country,
                Admin = customer.Admin,
                Comments = customer.Comments,
                //Subscriptions = customer.Subscriptions
            };
            return Ok(customerDto);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ReadWithSubscriptions(int id)
        {
            var customer = await customerRepository.ReadWithSubscriptions(id);
            if (customer is null)
            {
                return NotFound($"Customer Id {id} not exists ");
            }
            CustomerDto customerDto = new()
            {
                Name = customer.Name,
                Email = customer.Email,
                MobileNumber = customer.MobileNumber,
                Facebook = customer.Facebook,
                Gender = customer.Gender,
                Country = customer.Country,
                Admin = customer.Admin,
                Comments = customer.Comments,
                //Subscriptions = customer.Subscriptions
            };
            return Ok(customerDto);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto customerDTO)
        {
            var customerToUpdate =await customerRepository.ReadWithSubscriptions(id);

            if (customerToUpdate is null)
            {
                return NotFound($"Customer with Id {id} not exists ");
            }

            if (customerDTO is null)
            {
                return BadRequest("Customer is null");
            }

            customerToUpdate.Name = customerDTO.Name;
            customerToUpdate.Email = customerDTO.Email;
            customerToUpdate.MobileNumber = customerDTO.MobileNumber;
            customerToUpdate.Facebook = customerDTO.Facebook;
            customerToUpdate.Gender = customerDTO.Gender;
            customerToUpdate.Country = customerDTO.Country;
            customerToUpdate.Admin = customerDTO.Admin;
            customerToUpdate.Comments = customerDTO.Comments;
            ////customerToUpdate.Subscriptions = customerDTO.Subscriptions;
            //if(customerDTO.Subscriptions is null)
            //{
            //    customerToUpdate.Subscriptions.Clear();
            //}
            //else
            //{
            //    var updatedSubscriptions = customerDTO.Subscriptions.Select(s => s.Id);
            //    var removedSubscriptions = customerToUpdate.Subscriptions.Where(s => !updatedSubscriptions.Contains(s.Id)).ToList();
            //    foreach (var subscription in removedSubscriptions)
            //    {
            //        customerToUpdate.Subscriptions.Remove(subscription);
            //    }
            //    customerRepository.SaveChanges();
            //}

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

            customerRepository.Remove(customer);
            return Ok("Customer Removed successfully");
        }
    }
}
