using MachineAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.BusinessLogic
{
    public interface IMalfunctionService
    {
        Malfunction GetMalfunction(string malfunctionName);
        void AddMalfunction(Malfunction model);
        void UpdateMalfunction(string malfunctionName, Malfunction model);
        void DeleteMalfunction(string malfunctionName);
    }
}
