using MachineAPI.Models;

namespace MachineAPI.DataAccess

{
    public interface IDataRepository
    {
        
        Machine GetMachine(string machineName);
        void AddMachine(string machineName);
        void UpdateMachine(string malfunctionName, Machine model);
        void DeleteMachine(string machineName);

        Malfunction GetMalfunction(string malfunctionName);
        List<Malfunction> GetN_Malfunctions(int n, int offset);
        void AddMalfunction(Malfunction model);
        void UpdateMalfunction(string malfunctionName, Malfunction model);
        void DeleteMalfunction(string malfunctionName);

        void ChangeStatusMalfunction(string malfunctionName);

    }
}
