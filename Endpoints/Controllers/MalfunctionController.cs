using MachineService.DataAccess;
using MachineService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MachineService.Controllers
{
    [ApiController]
    public class MalfunctionController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IDataRepository _dataRepository;

        public MalfunctionController(IConfiguration config, IDataRepository dataRepository)
        {
            _config = config;
            _dataRepository = dataRepository;
        }

        [HttpPost]
        [Route("api/[controller]/add")]
        public Malfunction AddMalfunction(Malfunction model) => _dataRepository.AddMalfunction(model);

        [HttpGet]
        [Route("api/[controller]/get")]
        public Malfunction GetMalfunction(string malfunctionName) => _dataRepository.GetMalfunction(malfunctionName);


        [HttpDelete]
        [Route("api/[controller]/delete")]
        public void DeleteMalfunction(string malfunctionName) => _dataRepository.DeleteMalfunction(malfunctionName);


        [HttpPost]
        [Route("api/[controller]/update")]
        public Malfunction UpdateMalfunction(Malfunction model) => _dataRepository.UpdateMalfunction(model);

            

    
    }
}
