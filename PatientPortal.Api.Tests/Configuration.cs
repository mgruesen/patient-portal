using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PatientPortal.Api.Tests
{
    public static class Configuration
    {
        private static readonly IConfiguration _configuration;
        static Configuration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
        }

        public static IConfiguration GetConfiguration() => _configuration;
        public static string GetConnectionString()
        {
            var dbConfig = _configuration.GetSection("Database");
            return new SqlConnectionStringBuilder
            {
                DataSource = dbConfig["Host"],
                UserID = dbConfig["User"],
                Password = dbConfig["Password"]
            }.ToString();
        }
    }
}
