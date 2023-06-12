using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ReactRandomJokes.Data
{
    public class RJContextFactory : IDesignTimeDbContextFactory<RJDataContext>
    {
        public RJDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}ReactRandomJokes.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new RJDataContext(config.GetConnectionString("ConStr"));
        }
    }
}