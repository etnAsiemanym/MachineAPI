namespace MachineAPI.Models
{
    public class Machine
    {
        public string MachineName { get; set; }
        public List<Malfunction> Malfunctions { get; set; }
        public int AverageResolutionTimeInDays { get; set; }
    }
}
