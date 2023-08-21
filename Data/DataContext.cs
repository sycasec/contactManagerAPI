using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace contactManagerAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Config;
        public DataContext(IConfiguration config)
        {
            Config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Config.GetConnectionString("ContactManagerDatabase"));
        }
    }
}
