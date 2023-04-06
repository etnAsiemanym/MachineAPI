using MachineAPI.BusinessLogic;
using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.Controllers
{
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpPost]
        [Route("api/[controller]/add")]
        public async Task<IActionResult> AddMachine(string machineName)
        {
            try
            {
                await _machineService.AddMachine(machineName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/get")]
        public async Task<IActionResult> GetMachine(string machineName) 
        {
            try
            {
                var Machine = await _machineService.GetMachine(machineName);
                if (Machine == null)
                    return NotFound();

                return Ok(Machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/delete")]
        public async Task<IActionResult> DeleteMachine(string machineName)
        {
            try
            {
                await _machineService.DeleteMachine(machineName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/update")]
        public async Task<IActionResult> UpdateMachine(string machineName, Machine model)
        {
            try
            {
                await _machineService.UpdateMachine(machineName, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
