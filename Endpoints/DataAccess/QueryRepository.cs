namespace MachineAPI.DataAccess
{
    public class QueryRepository
    {
        private readonly IConfiguration _configuration;

        public QueryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetMachineQuery()
        {
            return "SELECT * FROM \"Machines\".machines where machine_name = @machineName";
        }
    }
}
