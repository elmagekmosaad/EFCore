using EFCore.Data.Enums;
using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.MySQL.Data;
using EFCore.MySQL.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAllSubscriptions()
        {
            var subscriptions =await subscriptionRepository.GetAll();

            return Ok(subscriptions);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(SubscriptionDto subscriptionDTO)
        {
            if (ModelState.IsValid)
            {
                var subscription = new Subscription
                {
                    Id = subscriptionDTO.Id,
                    Period = subscriptionDTO.Period,
                    StartOfSubscription = subscriptionDTO.StartOfSubscription,
                    EndOfSubscription = subscriptionDTO.EndOfSubscription,
                    Price = subscriptionDTO.Price,
                    Discount = subscriptionDTO.Discount,
                    CustomerId = subscriptionDTO.CustomerId
                };

               await subscriptionRepository.Add(subscription);

                return Ok("Subscription added successfully");
            }
            else
            {
                return BadRequest("Invalid model state");
            }
            
        }

        [HttpGet("[action]/{subscriptionId}")]
        public async Task<IActionResult> Read(int subscriptionId)
        {
            var subscription = await subscriptionRepository.GetById(subscriptionId);

            if (subscription is null)
            {
                return NotFound($"Subscription Id {subscriptionId} not exists ");
            }

            return Ok(subscription);
        }
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> ReadByCustomerId(int customerId)
        {
            var subscriptions = await subscriptionRepository.GetByCustomerId(customerId);

            if (subscriptions is null)
            {
                return NotFound($"Subscriptions With customerId {customerId} not exists ");
            }
            
            return Ok(subscriptions);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id,SubscriptionDto subscriptionDTO)
        {
            if (subscriptionDTO is null)
            {
                return BadRequest("Subscription is null");
            }
           
            var subscriptionToUpdate = await subscriptionRepository.GetById(id);

            if (subscriptionToUpdate is null)
            {
                return NotFound($"Subscription Id {id} not exists ");
            }

            subscriptionToUpdate.Period = subscriptionDTO.Period;
            subscriptionToUpdate.StartOfSubscription = subscriptionDTO.StartOfSubscription;
            subscriptionToUpdate.EndOfSubscription = subscriptionDTO.EndOfSubscription;
            subscriptionToUpdate.Price = subscriptionDTO.Price;
            subscriptionToUpdate.Discount = subscriptionDTO.Discount;
            subscriptionToUpdate.CustomerId = subscriptionDTO.CustomerId;

            await subscriptionRepository.Update(subscriptionToUpdate);

            return Ok("Subscription Updated successfully");

        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subscription = await subscriptionRepository.GetById(id);

            if (subscription is null)
            {
                return NotFound($"Subscription Id {id} not exists ");
            }
            await subscriptionRepository.Remove(subscription);

            return Ok("Subscription Removed successfully");
        }







    }
}
