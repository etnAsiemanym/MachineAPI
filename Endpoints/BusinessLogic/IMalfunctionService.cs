using MachineAPI.Models;

namespace MachineAPI.BusinessLogic
{
    public interface IMalfunctionService
    {
        Malfunction GetMalfunction(string malfunctionName);
        Malfunction AddMalfunction(Malfunction model);
        Malfunction UpdateMalfunction(Malfunction model);
        void DeleteMalfunction(string malfunctionName);
    }
}
