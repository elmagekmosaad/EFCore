using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.MySQL.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IMapper mapper;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAllSubscriptions()
        {
            var subscriptions = await subscriptionRepository.GetAll();
            var data = mapper.Map<IEnumerable<SubscriptionDto>> (subscriptions);
            return Ok(data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(SubscriptionDto subscriptionDto)
        {
            if (ModelState.IsValid)
            {
                var entity = mapper.Map<Subscription>(subscriptionDto);
               await subscriptionRepository.Add(entity);

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
            var entity = mapper.Map<SubscriptionDto>(subscription);

            return Ok(entity);
        }
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> ReadByCustomerId(int customerId)
        {
            var subscriptions = await subscriptionRepository.GetByCustomerId(customerId);

            if (subscriptions is null)
            {
                return NotFound($"Subscriptions With customerId {customerId} not exists ");
            }
            var data = mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);

            return Ok(data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id,SubscriptionDto subscriptionDto)
        {
            if (subscriptionDto is null)
            {
                return BadRequest("Subscription is null");
            }
           
            var subscriptionToUpdate = await subscriptionRepository.GetById(id);

            if (subscriptionToUpdate is null)
            {
                return NotFound($"Subscription Id {id} not exists ");
            }

            mapper.Map(subscriptionDto, subscriptionToUpdate);

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
