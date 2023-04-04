using Dapper;
using MachineAPI.Models;
using Npgsql;

namespace MachineAPI.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly IConfiguration _config;
        //private const string machineDB = _config.GetConnectionString("DefaultConnection");

        public DataRepository(IConfiguration config)
        {
            _config = config;
        }

        public void AddMachine(string machineName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Database=machineDatabase; User Id=ApiUser; password=test; "))
            {


                connection.Open();
                connection.Query($"INSERT INTO \"Machines\".machines(machine_name) VALUES ('{machineName}');");

            }
        }

        public Malfunction AddMalfunction(Malfunction model)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Database=machineDatabase; User Id=ApiUser; password=test; "))
            {
                connection.Open();
                dynamic malfunction = connection.Query($"INSERT INTO \"Machines\".malfunctions(machine_id, description, priority, created_date) VALUES ({@model.MachineId}, {@model.Description}, {@model.Priority},{@model.CreatedDate});");

                malfunction = connection.Query($"select * from \"Machines\".machines where machine_name = '{model.MachineId}'", new { id = 0, machine_name = "name" }).FirstOrDefault();

                if (malfunction != null)
                {
                    model.Id = malfunction.id;
                    model.MachineId = malfunction.machine_id;
                }
                return model;
            }
        }

        public void DeleteMachine(string machineName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Database=machineDatabase; User Id=ApiUser; password=test; "))
            {
                connection.Open();
                var machine = connection.Query($"DELETE FROM \"Machines\".machines WHERE machine_name = '{machineName}'");


            }
        }

        public void DeleteMalfunction(string malfunctionName)
        {
            throw new NotImplementedException();
        }

        public Machine GetMachine(string machineName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Database=machineDatabase; User Id=ApiUser; password=test; "))
            {
                Machine model = new Machine();
                connection.Open();
                dynamic lines = connection.Query($"select * from \"Machines\".machines where id = {id}", new { id = 0, machine_name = "name" }).FirstOrDefault();

                if (lines != null)
                {
                    model.Id = lines.id;
                    model.MachineName = lines.machine_name;
                    //Console.WriteLine(lines);
                }
                //Console.WriteLine(model.MachineName);

                return model;
            }
        }

        public Malfunction GetMalfunction(string malfunctionName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Database=machineDatabase; User Id=ApiUser; password=test; "))
            {
                Malfunction model = new Malfunction();
                connection.Open();
                var lines = connection.Query($"select * from \"Machines\".malfunctions where id = {malfunctionName}",
                    new
                    {
                        id = 1,
                        machine_id = 0,
                        description = "desc",
                        priority = "low",
                        created_date = new DateTime(),
                        closed_date = new DateTime(),
                        is_fixed = false
                    }).FirstOrDefault();

                if (lines != null)
                {
                    model.Id = lines.id;
                    model.MachineId = lines.machine_id;
                    model.Description = lines.description;
                    model.Priority = lines.priority;
                    model.CreatedDate = lines.created_date;
                    if (lines.is_fixed == null)
                    {
                        model.IsFixed = false;
                    }
                    else
                    {
                        model.ClosedDate = lines.closed_date;
                        model.IsFixed = lines.is_fixed;
                    }
                }
                //Console.WriteLine(model.MachineName);

                return model;
            }
        }

        public Machine UpdateMachine(Machine model)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Database=machineDatabase; User Id=ApiUser; password=test; "))
            {
                connection.Open();
                dynamic lines = connection.Query($"UPDATE \"Machines\".machines SET machine_name = '{model.MachineName}' WHERE id = '{model.Id}'");
                lines = connection.Query($"select * from \"Machines\".machines where id = {model.Id}", new { id = 0, machine_name = "name" }).FirstOrDefault();

                if (lines != null)
                {
                    model.Id = lines.id;
                    model.MachineName = lines.machine_name;
                }
                return model;
            }
        }

        public Malfunction UpdateMalfunction(Malfunction model)
        {
            throw new NotImplementedException();
        }
    }
}
