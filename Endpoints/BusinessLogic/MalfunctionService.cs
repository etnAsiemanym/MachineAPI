using MachineAPI.DataAccess;
using MachineAPI.Models;

namespace MachineAPI.BusinessLogic
{
    public class MalfunctionService : IMalfunctionService
    {
        private readonly IDataRepository _dataRepository;

        public MalfunctionService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public Task AddMalfunction(Malfunction model) => _dataRepository.AddMalfunction(model);
        public Task<Malfunction> GetMalfunction(string malfunctionName) => _dataRepository.GetMalfunction(malfunctionName);
        public Task<List<Malfunction>> GetN_Malfunctions(int n, int offset) => _dataRepository.GetN_Malfunctions(n, offset); 
        public Task DeleteMalfunction(string malfunctionName) => _dataRepository.DeleteMalfunction(malfunctionName);
        public Task UpdateMalfunction(string malfunctionName, Malfunction model) => _dataRepository.UpdateMalfunction(model.MalfunctionName, model);
        public Task ChangeStatusMalfunction(string malfunctionName) => _dataRepository.ChangeStatusMalfunction(malfunctionName);
    }
}
