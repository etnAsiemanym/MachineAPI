using MachineService.DataAccess;
using MachineService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineService.Controllers
{
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IDataRepository _dataRepository;

        public MachineController(IConfiguration config, IDataRepository dataRepository)
        {
            _config = config;
            _dataRepository = dataRepository;
        }

        [HttpPost]
        [Route("api/[controller]/add")]
        public Machine AddMachine(string machineName) => _dataRepository.AddMachine(machineName);


        [HttpGet]
        [Route("api/[controller]/get")]
        public Machine GetMachine(string machineName) => _dataRepository.GetMachine(machineName);




        [HttpDelete]
        [Route("api/[controller]/delete")]
        public void DeleteMachine(string machineName) => _dataRepository.DeleteMachine(machineName);


        [HttpPost]
        [Route("api/[controller]/update")]
        public Machine UpdateMachine(Machine model) => _dataRepository.UpdateMachine(model);

    }
}
