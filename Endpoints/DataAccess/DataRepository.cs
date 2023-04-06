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

        public async Task AddMachine(string machineName)
        {
            var query = "INSERT INTO \"Machines\".machines(machine_name) VALUES(@machine_name)";
            var parameters = new DynamicParameters();
            parameters.Add("machine_name", machineName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task AddMalfunction(Malfunction model)
        {
            using (var connection = _context.CreateConnection())
            {
                var machineName = model.MachineName;
                var query = "SELECT machine_name FROM \"Machines\".malfunctions WHERE isfixed = False AND machine_name = @machineName";
                if (!connection.Query(query, new { machineName }).Contains(false)) // doesn't work quite right
                {
                    if (model.Description != "")
                    {
                        query = "INSERT INTO \"Machines\".malfunctions(malfunction_name, machine_name, description, priority, created_date)" +
                            " VALUES(@malfunction_name, @machine_name, @description, @priority, @created_date)";
                        var parameters = new DynamicParameters();
                        parameters.Add("malfunction_name", model.MalfunctionName, DbType.String);
                        parameters.Add("machine_name", model.MachineName, DbType.String);                        
                        parameters.Add("description", model.Description, DbType.String);
                        parameters.Add("created_date", model.CreatedDate, DbType.DateTime);

                        if (model.Priority.ToLower() == "low" || model.Priority.ToLower() == "mid" || model.Priority.ToLower() == "high")
                        {
                            parameters.Add("priority", model.Priority.ToLower(), DbType.String);
                        }
                        else parameters.Add("priority", "mid", DbType.String);                      

                        await connection.ExecuteAsync(query, parameters);
                    }                  
                }
            }
        }

        public async Task DeleteMachine(string machineName)
        {
            var query = "DELETE FROM \"Machines\".machines WHERE machine_name = @machineName";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { machineName });
            }
        }

        public async Task DeleteMalfunction(string malfunctionName)
        {
            var query = "DELETE FROM \"Machines\".malfunctions WHERE malfunction_name = @malfunctionName";
            using (var connection = _context.CreateConnection())
            {
               await connection.ExecuteAsync(query, new { malfunctionName });
            }
        }

        public async Task<Machine> GetMachine(string machineName)
        {
            var query = "SELECT machine_name AS machineName FROM \"Machines\".machines where machine_name = @machineName";
            using (var connection = _context.CreateConnection())
            {
                var machine = await connection.QuerySingleOrDefaultAsync<Machine>(query, new { machineName });
                machine.Malfunctions = await GetMalfunctions(machineName);
                machine.AverageResolutionTimeInDays = await CalculateResolutionTime(machine.Malfunctions);
                return machine;
            }
        }

        public async Task<int> CalculateResolutionTime(List<Malfunction> malfunctions)
        {
            int totalTime = 0;
            foreach (var malfunction in malfunctions)
            {
                totalTime += Convert.ToInt32((malfunction.ClosedDate - malfunction.CreatedDate).TotalDays);
            }
            return totalTime / malfunctions.Count;
        }

        public async Task<Malfunction> GetMalfunction(string malfunctionName)
        {
            var query = "SELECT malfunction_name AS malfunctionName, machine_name AS machineName, description, priority, created_date AS createdDate, closed_date AS closedDate," +
                        "isfixed FROM \"Machines\".malfunctions WHERE malfunction_name = @malfunctionName";
            using (var connection = _context.CreateConnection())
            {
                var malfunction = connection.QuerySingleOrDefaultAsync<Malfunction>(query, new { malfunctionName });
                return await malfunction;
            }
        }

        public async Task<List<Malfunction>> GetMalfunctions(string machineName)
        {
            var query = "SELECT malfunction_name AS malfunctionName, machine_name AS machineName, description, priority, created_date AS createdDate, closed_date AS closedDate," +
                        "isfixed FROM \"Machines\".malfunctions WHERE machine_name = @machineName";
            using (var connection = _context.CreateConnection())
            {
                var malfunctions = await connection.QueryAsync<Malfunction>(query, new { machineName });
                return malfunctions.ToList();
            }
        }

        public async Task<List<Malfunction>> GetN_Malfunctions(int n, int offset)
        {
            var query = "SELECT * FROM (SELECT malfunction_name AS malfunctionName, machine_name AS machineName, description, LOWER(priority) as priority, created_date AS createdDate, closed_date AS closedDate," +
                        $"isfixed FROM \"Machines\".malfunctions)sub ORDER BY CASE WHEN (priority = 'low') THEN 1 WHEN (priority = 'mid') THEN 2 WHEN (priority = 'high') THEN 3 END LIMIT {n} OFFSET {offset};";
            using (var connection = _context.CreateConnection())
            {
                     var malfunctions = await connection.QueryAsync<Malfunction>(query);
                     return malfunctions.ToList();
            }
        }

        public async Task UpdateMachine(string machineName, Machine model)
        {
            var query = "UPDATE \"Machines\".machines SET machine_name = @machine_name WHERE machine_name = @machine_name ";
            var parameters = new DynamicParameters();
            parameters.Add("machine_name", machineName, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                    await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateMalfunction(string malfunctionName, Malfunction model)
        {
            var query = "UPDATE \"Machines\".malfunctions SET description = @description," +
                        " priority = @priority, closed_date = @closed_date, isfixed = @isfixed WHERE malfunction_name = @malfunction_name";
            var parameters = new DynamicParameters();
            parameters.Add("malfunction_name", malfunctionName, DbType.String);
            parameters.Add("description", model.Description, DbType.String);
            parameters.Add("closed_date", model.ClosedDate, DbType.DateTime);
            parameters.Add("isfixed", model.IsFixed, DbType.Boolean);

            if (model.Priority.ToLower() == "low" || model.Priority.ToLower() == "mid" || model.Priority.ToLower() == "high") 
            {
                parameters.Add("priority", model.Priority, DbType.String); 
            }
            else parameters.Add("priority", "mid", DbType.String);
                       
            using (var connection = _context.CreateConnection())
            {
                  await connection.ExecuteAsync(query, parameters);
            }         
        }

        public async Task ChangeStatusMalfunction(string malfunctionName)
        {
            var query = "UPDATE \"Machines\".malfunctions SET closed_date = @closed_date, isfixed = @isfixed WHERE malfunction_name = @malfunction_name";
            var parameters = new DynamicParameters();
            parameters.Add("malfunction_name", malfunctionName, DbType.String);
            parameters.Add("closed_date", DateTime.UtcNow , DbType.DateTime);
            parameters.Add("isfixed", true, DbType.Boolean);

            using (var connection = _context.CreateConnection())
            {
                var malfunction = await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
