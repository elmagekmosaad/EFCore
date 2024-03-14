using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.MySQL.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IComputerRepository computerRepository;
        private readonly IMapper mapper;

        public ComputerController(IComputerRepository computerRepository,IMapper mapper)
        {
            this.computerRepository = computerRepository;
            this.mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> GetAllComputers()
        {
            var computers = await computerRepository.GetAll();
            var data = mapper.Map<IEnumerable<ComputerDto>>(computers);

            return Ok(data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(ComputerDto ComputerDto)
        {
            if (ModelState.IsValid)
            {
                var entity = mapper.Map<Computer>(ComputerDto);
                await computerRepository.Add(entity);

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
            var entity = mapper.Map<ComputerDto>(Computer);
            return Ok(entity);
        }
        [HttpGet("[action]/{subscriptionId}")]
        public async Task<IActionResult> ReadBySubscriptionId(int subscriptionId)
        {
            var Computer = await computerRepository.GetBySubscriptionId(subscriptionId);

            if (Computer is null)
            {
                return NotFound($"Computer With subscriptionId {subscriptionId} not exists ");
            }
            var entity = mapper.Map<ComputerDto>(Computer);
            return Ok(entity);
        }
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> ReadByCustomerId(int customerId)
        {
            var Computers = await computerRepository.GetByCustomerId(customerId);

            if (Computers is null)
            {
                return NotFound($"Computers With customerId {customerId} not exists ");
            }
            var entities = mapper.Map<IEnumerable<ComputerDto>>(Computers);
            return Ok(entities);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, ComputerDto ComputerDto)
        {
            if (ComputerDto is null)
            {
                return BadRequest("Computer is null");
            }

            var ComputerToUpdate = await computerRepository.GetById(id);

            if (ComputerToUpdate is null)
            {
                return NotFound($"Computer Id {id} not exists ");
            }

            mapper.Map(ComputerDto, ComputerToUpdate);

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
