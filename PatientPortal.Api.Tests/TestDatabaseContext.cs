using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PatientPortal.Api.Tests
{
    public class TestDatabaseContext : IDisposable
    {
        private static readonly object _lock = new();
        public DbConnection Connection { get; }

        public TestDatabaseContext()
        {
            Connection = new SqlConnection(Configuration.GetConnectionString());
            Connection.Open();
        }

        public PatientPortalDbContext CreateDbContext(DbTransaction transaction = null)
        {
            var context = new PatientPortalDbContext(
                new DbContextOptionsBuilder<PatientPortalDbContext>()
                    .UseSqlServer(Connection).Options);

            if (transaction != null)
                context.Database.UseTransaction(transaction);

            return context;
        }

        public void Dispose() => Connection.Dispose();
    }
}
