using MachineAPI.DataAccess;
using MachineAPI.Models;

namespace MachineAPI.BusinessLogic
{
    public class MachineService : IMachineService
    {
        private readonly IDataRepository _dataRepository;

        public MachineService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void AddMachine(string machineName)
        {
            if (!string.IsNullOrEmpty(machineName)) _dataRepository.AddMachine(machineName);
        }
        public Machine GetMachine(string machineName) => _dataRepository.GetMachine(machineName);

        public void DeleteMachine(string machineName) => _dataRepository.DeleteMachine(machineName);

        public void UpdateMachine(string machineName, Machine model) => _dataRepository.UpdateMachine(machineName, model);

        public Machine AssignMalfunctions(Machine model)
        {
            var query = "SELECT * FROM \"Machines\".malfunctions as ma where machine_name = @machineName ma JOIN malfunctions mf ON m.machine_name = mf.machine_name";
            return model;
        }
    }
}
