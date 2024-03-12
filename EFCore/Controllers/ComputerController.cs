using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.Models.Repository;
using EFCore.MySQL.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IComputerRepository computerRepository;
        public ComputerController(IComputerRepository computerRepository)
        {
            this.computerRepository = computerRepository;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> GetAllComputers()
        {
            var computers = await computerRepository.GetAll();
            return Ok(computers);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(ComputerDto ComputerDTO)
        {
            if (ModelState.IsValid)
            {
                var Computer = new Computer
                {
                    Id = ComputerDTO.Id,
                    Hwid = ComputerDTO.Hwid,
                    Serial = ComputerDTO.Serial,
                    SubscriptionId = ComputerDTO.SubscriptionId,
                    CustomerId = ComputerDTO.CustomerId
                };

                await computerRepository.Add(Computer);

                return Ok("Computer added successfully");
            }
            else
            {
                return BadRequest("Invalid model state");
            }

        }

        [HttpGet("[action]/{ComputerId}")]
        public async Task<IActionResult> Read(int ComputerId)
        {
            var Computer = await computerRepository.GetById(ComputerId);

            if (Computer is null)
            {
                return NotFound($"Computer Id {ComputerId} not exists ");
            }

            return Ok(Computer);
        }
        [HttpGet("[action]/{subscriptionId}")]
        public async Task<IActionResult> ReadBySubscriptionId(int subscriptionId)
        {
            var Computers = await computerRepository.GetBySubscriptionId(subscriptionId);

            if (Computers is null)
            {
                return NotFound($"Computers With subscriptionId {subscriptionId} not exists ");
            }

            return Ok(Computers);
        }
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> ReadByCustomerId(int customerId)
        {
            var Computers = await computerRepository.GetByCustomerId(customerId);

            if (Computers is null)
            {
                return NotFound($"Computers With customerId {customerId} not exists ");
            }

            return Ok(Computers);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, ComputerDto ComputerDTO)
        {
            if (ComputerDTO is null)
            {
                return BadRequest("Computer is null");
            }

            var ComputerToUpdate = await computerRepository.GetById(id);

            if (ComputerToUpdate is null)
            {
                return NotFound($"Computer Id {id} not exists ");
            }

            ComputerToUpdate.Hwid = ComputerDTO.Hwid;
            ComputerToUpdate.Serial = ComputerDTO.Serial;
            ComputerToUpdate.SubscriptionId = ComputerDTO.SubscriptionId;

            await computerRepository.Update(ComputerToUpdate);

            return Ok("Computer Updated successfully");

        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Computer = await computerRepository.GetById(id);

            if (Computer is null)
            {
                return NotFound($"Computer Id {id} not exists ");
            }
            await computerRepository.Remove(Computer);

            return Ok("Computer Removed successfully");
        }
    }
}
