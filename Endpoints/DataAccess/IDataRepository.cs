using MachineAPI.Models;

namespace MachineAPI.DataAccess

{
    public interface IDataRepository
    {
        
        public Task<Machine> GetMachine(string machineName);
        public Task AddMachine(string machineName);
        public Task UpdateMachine(string malfunctionName, Machine model);
        public Task DeleteMachine(string machineName);

        public Task<Malfunction> GetMalfunction(string malfunctionName);
        public Task<List<Malfunction>> GetN_Malfunctions(int n, int offset);
        public Task AddMalfunction(Malfunction model);
        public Task UpdateMalfunction(string malfunctionName, Malfunction model);
        public Task DeleteMalfunction(string malfunctionName);
        public Task ChangeStatusMalfunction(string malfunctionName);

    }
}
