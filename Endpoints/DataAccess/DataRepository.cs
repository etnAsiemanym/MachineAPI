using Dapper;
using MachineAPI.Context;
using MachineAPI.Models;
using System.Data;

namespace MachineAPI.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly DapperContext _context;
        public DataRepository(DapperContext context)
        {
            _context = context;
        }

        public void AddMachine(string machineName)
        {
            var query = "INSERT INTO \"Machines\".machines(machine_name) VALUES(@machine_name)";
            var parameters = new DynamicParameters();
            parameters.Add("machine_name", machineName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, parameters);
            }
        }

        public void AddMalfunction(Malfunction model)
        {
            var query = "INSERT INTO \"Machines\".malfunctions(malfunction_name, machine_name, description, priority, created_date)" +
                        " VALUES(@malfunction_name, @machine_name, @description, @priority, @created_date)";
            var parameters = new DynamicParameters();
            parameters.Add("malfunction_name", model.MalfuctionName, DbType.String);
            parameters.Add("machine_name", model.MachineName, DbType.String);
            parameters.Add("description", model.Description, DbType.String);
            parameters.Add("priority", model.Priority, DbType.String);
            parameters.Add("created_date", model.CreatedDate, DbType.DateTime);
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, parameters);
            }
        }

        public void DeleteMachine(string machineName)
        {
            var query = "DELETE FROM \"Machines\".machines WHERE machine_name = @machineName";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, new { machineName });
            }
        }

        public void DeleteMalfunction(string malfunctionName)
        {
            var query = "DELETE FROM \"Machines\".malfunctions WHERE malfunction_name = @malfunctionName";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, new { malfunctionName });
            }
        }

        public Machine GetMachine(string machineName)
        {
            var query = "SELECT * FROM \"Machines\".machines where machine_name = @machineName";
            using (var connection = _context.CreateConnection())
            {
                var machine = connection.QuerySingleOrDefault<Machine>(query, new { machineName });
                machine.Malfunctions = GetMalfunctions(machineName);
                machine.AverageResolutionTimeInDays = CalculateResolutionTime(machine.Malfunctions);
                return machine;
            }
        }

        public int CalculateResolutionTime(List<Malfunction> malfunctions)
        {
            int totalTime = 0;
            foreach (var malfunction in malfunctions)
            {
                totalTime += Convert.ToInt32((malfunction.ClosedDate - malfunction.CreatedDate).TotalDays);
            }

            

            return totalTime / malfunctions.Count;
        }

        public Malfunction GetMalfunction(string malfunctionName)
        {
            var query = "SELECT malfunction_name AS malfunctionName, machine_name AS machineName, description, priority, created_date AS createdDate, closed_date AS closedDate," +
                        "isfixed FROM \"Machines\".malfunctions WHERE malfunction_name = @malfunctionName";
            using (var connection = _context.CreateConnection())
            {
                var malfunction = connection.QuerySingleOrDefault<Malfunction>(query, new { malfunctionName });
                return malfunction;
            }
        }

        public List<Malfunction> GetMalfunctions(string machineName)
        {
            var query = "SELECT malfunction_name AS malfunctionName, machine_name AS machineName, description, priority, created_date AS createdDate, closed_date AS closedDate," +
                        "isfixed FROM \"Machines\".malfunctions WHERE machine_name = @machineName";
            using (var connection = _context.CreateConnection())
            {
                List<Malfunction> malfunctionList = new List<Malfunction>();
                var malfunctions = connection.Query<Malfunction>(query);
                malfunctions = malfunctions.ToList();
                int index = 0;
                foreach (var malfunction in malfunctions)
                {
                    malfunctionList[index].MalfuctionName = malfunction.MalfuctionName;
                    malfunctionList[index].MachineName = machineName; 
                    malfunctionList[index].Description = malfunction.Description;
                    malfunctionList[index].Priority = malfunction.Priority;
                    malfunctionList[index].CreatedDate = malfunction.CreatedDate;
                    malfunctionList[index].ClosedDate = malfunction.ClosedDate;
                    malfunctionList[index].IsFixed = malfunction.IsFixed;
                    index ++;
                }

                return malfunctionList;
            }
        }

        public void UpdateMachine(string machineName, Machine model)
        {
            var query = "UPDATE \"Machines\".machines SET machine_name = @machine_name WHERE machine_name = @machine_name ";
            var parameters = new DynamicParameters();
            parameters.Add("machine_name", machineName, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                    connection.Execute(query, parameters);
            }
        }

        public void UpdateMalfunction(string malfunctionName, Malfunction model)
        {
            var query = "UPDATE \"Machines\".malfunctions SET description = @description," +
                        " priority = @priority, closed_date = @closed_date, isfixed = @isfixed WHERE malfunction_name = @malfunction_name";
            var parameters = new DynamicParameters();
            parameters.Add("malfunction_name", malfunctionName, DbType.String);
            parameters.Add("description", model.Description, DbType.String);
            parameters.Add("priority", model.Priority, DbType.String);
            parameters.Add("closed_date", model.ClosedDate, DbType.DateTime);
            parameters.Add("isfixed", model.IsFixed, DbType.Boolean);
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, parameters);
            }
        }
    }
}
