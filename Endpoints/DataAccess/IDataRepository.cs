using MachineService.Models;

namespace MachineService.DataAccess

{
    public interface IDataRepository
    {
        Machine GetMachine(string machineName);

        Machine AddMachine(string machineName);

        Machine UpdateMachine(Machine model);

        void DeleteMachine(string machineName);

        Malfunction GetMalfunction(string malfunctionName);
        Malfunction AddMalfunction(Malfunction model);
        Malfunction UpdateMalfunction(Malfunction model);
        void DeleteMalfunction(string malfunctionName);
        
    }
}
