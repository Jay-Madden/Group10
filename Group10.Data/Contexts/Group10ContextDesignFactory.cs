using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Group10.Data.Contexts
{
    public class Group10ContextDesignFactory: IDesignTimeDbContextFactory<Group10Context> 
    {
        public Group10Context CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{Directory.GetCurrentDirectory()}/../Group10.API/appsettings.Local.json")
                .Build();

            var builder = new DbContextOptionsBuilder<Group10Context>();
            builder.UseNpgsql(configuration.GetConnectionString("Group10ConnectionString"));
            
            return new Group10Context(builder.Options);
        }
    }
}
