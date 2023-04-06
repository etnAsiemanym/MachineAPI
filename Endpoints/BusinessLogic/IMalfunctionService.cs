using MachineAPI.Models;

namespace MachineAPI.BusinessLogic
{
    public interface IMalfunctionService
    {
        Task<Malfunction> GetMalfunction(string malfunctionName);
        Task<List<Malfunction>> GetN_Malfunctions(int n, int offset);
        Task AddMalfunction(Malfunction model);
        Task UpdateMalfunction(string malfunctionName, Malfunction model);
        Task DeleteMalfunction(string malfunctionName);
        Task ChangeStatusMalfunction(string malfunctionName);
    }
}
