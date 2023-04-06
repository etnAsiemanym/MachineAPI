using MachineAPI.Models;

namespace MachineAPI.BusinessLogic
{
    public interface IMalfunctionService
    {
        Malfunction GetMalfunction(string malfunctionName);
        List<Malfunction> GetN_Malfunctions(int n, int offset);
        void AddMalfunction(Malfunction model);
        void UpdateMalfunction(string malfunctionName, Malfunction model);
        void DeleteMalfunction(string malfunctionName);
        void ChangeStatusMalfunction(string malfunctionName);
    }
}
