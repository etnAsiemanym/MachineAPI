using MachineAPI.Models;

namespace MachineAPI.BusinessLogic
{
    public interface IMachineService
    {
        Machine GetMachine(string machineName);
        void AddMachine(string machineName);
        void UpdateMachine(string machineName, Machine model);
        void DeleteMachine(string machineName);

        Machine AssignMalfunctions(Machine model);

    }
}
