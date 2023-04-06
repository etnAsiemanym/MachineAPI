using MachineAPI.BusinessLogic;
using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

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
        public void AddMalfunction(Malfunction model) => _malfunctionService.AddMalfunction(model);

        [HttpGet]
        [Route("api/[controller]/get")]
        public Malfunction GetMalfunction(string malfunctionName) => _malfunctionService.GetMalfunction(malfunctionName);    
        
        [HttpDelete]
        [Route("api/[controller]/delete")]
        public void DeleteMalfunction(string malfunctionName) => _malfunctionService.DeleteMalfunction(malfunctionName);

        [HttpPost]
        [Route("api/[controller]/update")]
        public void UpdateMalfunction(string malfunctionName, Malfunction model) => _malfunctionService.UpdateMalfunction(malfunctionName, model);
        
    }
}
