using MachineAPI.Models;
using System.Threading.Tasks;

namespace MachineAPI.BusinessLogic
{
    public interface IMachineService
    {
        Task<Machine> GetMachine(string machineName);
        Task AddMachine(string machineName);
        Task UpdateMachine(string machineName, Machine model);
        Task DeleteMachine(string machineName);
    }
}
