using MachineAPI.BusinessLogic;
using MachineAPI.Models;
using MachineService.DataAccess;
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
        public void AddMachine(string machineName) => _machineService.AddMachine(machineName);

        [HttpGet]
        [Route("api/[controller]/get")]
        public Machine GetMachine(string machineName) => _machineService.GetMachine(machineName);

        [HttpDelete]
        [Route("api/[controller]/delete")]
        public void DeleteMachine(string machineName) => _machineService.DeleteMachine(machineName);

        [HttpPost]
        [Route("api/[controller]/update")]
        public void UpdateMachine(Machine model) => _machineService.UpdateMachine(model);
    }
}
