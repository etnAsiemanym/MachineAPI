using MachineAPI.DataAccess;
using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.BusinessLogic
{
    public class MalfunctionService : IMalfunctionService
    {
        private readonly IDataRepository _dataRepository;

        public MalfunctionService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public void AddMalfunction(Malfunction model) => _dataRepository.AddMalfunction(model);
        public Malfunction GetMalfunction(string malfunctionName) => _dataRepository.GetMalfunction(malfunctionName);
        public List<Malfunction> GetN_Malfunctions(int n, int offset) => _dataRepository.GetN_Malfunctions(n, offset); 
        public void DeleteMalfunction(string malfunctionName) => _dataRepository.DeleteMalfunction(malfunctionName);
        public void UpdateMalfunction(string malfunctionName, Malfunction model) => _dataRepository.UpdateMalfunction(model.MalfunctionName, model);
        public void ChangeStatusMalfunction(string malfunctionName) => _dataRepository.ChangeStatusMalfunction(malfunctionName);
    }
}
