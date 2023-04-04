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
        public Malfunction AddMalfunction(Malfunction model) => _malfunctionService.AddMalfunction(model);

        [HttpGet]
        [Route("api/[controller]/get")]
        public Malfunction GetMalfunction(string malfunctionName) => _malfunctionService.GetMalfunction(malfunctionName);

        [HttpDelete]
        [Route("api/[controller]/delete")]
        public void DeleteMalfunction(string malfunctionName) => _malfunctionService.DeleteMalfunction(malfunctionName);

        [HttpPost]
        [Route("api/[controller]/update")]
        public Malfunction UpdateMalfunction(Malfunction model) => _malfunctionService.UpdateMalfunction(model);
    }
}
