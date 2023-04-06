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

        public Task AddMachine(string machineName) => _dataRepository.AddMachine(machineName);

        public Task<Machine> GetMachine(string machineName) => _dataRepository.GetMachine(machineName);

        public Task DeleteMachine(string machineName) => _dataRepository.DeleteMachine(machineName);

        public Task UpdateMachine(string machineName, Machine model) => _dataRepository.UpdateMachine(machineName, model);
    }
}
