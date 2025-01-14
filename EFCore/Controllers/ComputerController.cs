using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Dtos;
using EFCore.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(policy: Policies.SuperAdmin)]
    //[Authorize(policy: Policies.Admin)]
    //[Authorize(policy: Policies.Customer)]
    //[AuthorizeOnAnyOnePolicy($"{Policies.SuperAdmin}, {Policies.Admin}, {Policies.Customer}")]
    [AllowAnonymous]

    public class ComputerController : ControllerBase
    {
        private readonly IComputerRepository computerRepository;
        private readonly IMapper _mapper;

        public ComputerController(IComputerRepository computerRepository, IMapper mapper)
        {
            this.computerRepository = computerRepository;
            this._mapper = mapper;
        }
        [HttpGet("GetAll")]
        //[Authorize(Roles = Roles.Customer)]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> GetAllComputers()
        {
            var computers = await computerRepository.GetAll();
            var data = _mapper.Map<IEnumerable<ComputerDto>>(computers);

            return Ok(data);
        }
         [HttpGet("mvc")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> testMvc()
        {
            var computers = await computerRepository.GetAll();
            var data = _mapper.Map<IEnumerable<ComputerDto>>(computers);

            return Ok(data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(ComputerDto ComputerDto)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Computer>(ComputerDto);
                await computerRepository.Add(entity);

                return Ok("Computer added successfully");
            }
            else
            {
                return BadRequest("Invalid model state");
            }

        }

        [HttpGet("[action]/{ComputerId}")]
        public async Task<IActionResult> Read(string ComputerId)
        {
            var Computer = await computerRepository.GetById(ComputerId);

            if (Computer is null)
            {
                return NotFound($"Computer Id {ComputerId} not exists ");
            }
            var entity = _mapper.Map<ComputerDto>(Computer);
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
            var entity = _mapper.Map<ComputerDto>(Computer);
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
            var entities = _mapper.Map<IEnumerable<ComputerDto>>(Computers);
            return Ok(entities);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(string id, ComputerDto ComputerDto)
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

            _mapper.Map(ComputerDto, ComputerToUpdate);

            await computerRepository.Update(ComputerToUpdate);

            return Ok("Computer Updated successfully");

        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
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
