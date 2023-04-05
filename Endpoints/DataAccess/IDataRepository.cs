using MachineAPI.Context;
using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.DataAccess

{
    public interface IDataRepository
    {
        
        Machine GetMachine(string machineName);
        void AddMachine(string machineName);
        void UpdateMachine(string malfunctionName, Machine model);
        void DeleteMachine(string machineName);

        Malfunction GetMalfunction(string malfunctionName);
        void AddMalfunction(Malfunction model);
        void UpdateMalfunction(string malfunctionName, Malfunction model);
        void DeleteMalfunction(string malfunctionName);

    }
}
