﻿namespace MachineAPI.Models
{
    public class Malfunction
    {
        public string MalfunctionName { get; set; }

        public string MachineName { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ClosedDate { get; set; }

        public bool IsFixed { get; set; }

    }
}
