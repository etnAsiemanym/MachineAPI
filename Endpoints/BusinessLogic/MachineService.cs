using MachineAPI.DataAccess;
using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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

        public void UpdateMachine(Machine model) => _dataRepository.UpdateMachine(model);
    }
}
