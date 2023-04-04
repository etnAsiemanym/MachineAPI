using System.Security.Cryptography.X509Certificates;

namespace MachineService.Models
{
    public class Machine
    {
        public string MachineName { get; set; }
        public List<Malfunction> Malfunctions { get; set; }
        public int AverageResolutionTime { get; set; }
    }
}
