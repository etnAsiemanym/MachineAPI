using Dapper;
using MachineAPI.Models;
using MachineAPI.Context;
using System.Data;
using Microsoft.AspNetCore.Mvc;

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
                connection.Execute(query, new { machineName});
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
                return machine;
            }
        }

        public Malfunction GetMalfunction(string malfunctionName)
        {
            var query = "SELECT * FROM \"Machines\".malfunctions where malfunction_name = @malfunctionName";
            using (var connection = _context.CreateConnection())
            {
                var malfunction = connection.QuerySingleOrDefault<Malfunction>(query, new { malfunctionName });
                return malfunction;
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
                connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
