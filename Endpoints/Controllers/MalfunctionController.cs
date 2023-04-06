using MachineAPI.BusinessLogic;
using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.Controllers
{
    [ApiController]
    public class MalfunctionController : ControllerBase
    {
        private readonly IMalfunctionService _malfunctionService;

        public MalfunctionController(IMalfunctionService malfunctionService)
        {
            _malfunctionService = malfunctionService;
        }

        [HttpPost]
        [Route("api/[controller]/add")]
        public async Task<IActionResult> AddMalfunction(Malfunction model)
        {
            try
            {
                await _malfunctionService.AddMalfunction(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/get")]
        public async Task<IActionResult> GetMalfunction(string malfunctionName)
        {
            try
            {
                var Malfunction = await _malfunctionService.GetMalfunction(malfunctionName) ;
                if (Malfunction == null)
                    return NotFound();

                return Ok(Malfunction);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/get_n")]
        public async Task<IActionResult> GetN_Malfunctions(int n, int offset)
        {
            try
            {
                var Malfunctions = await _malfunctionService.GetN_Malfunctions(n, offset);
                if (Malfunctions == null)
                    return NotFound();

                return Ok(Malfunctions);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("api/[controller]/delete")]
        public async Task<IActionResult> DeleteMalfunction(string malfunctionName)
        {
            try
            {
                await _malfunctionService.DeleteMalfunction(malfunctionName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/[controller]/update")]
        public async Task<IActionResult> UpdateMalfunction(string malfunctionName, Malfunction model)
        {
            try
            {
                await _malfunctionService.UpdateMalfunction(malfunctionName, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/changestatus")]
        public async Task<IActionResult> ChangeStatusMalfunction(string malfunctionName)
                    {
            try
            {
                await _malfunctionService.ChangeStatusMalfunction(malfunctionName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
