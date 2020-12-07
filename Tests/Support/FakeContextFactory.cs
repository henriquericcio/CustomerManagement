using System;
using System.Data.Common;
using System.Threading.Tasks;
using CustomerManagement.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Tests.Support
{
    public class FakeContextFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<CustomerManagementContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseSqlite(_connection).Options;
        }

        public async Task<CustomerManagementContext> CreateContextAsync()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                await _connection.OpenAsync();

                var options = CreateOptions();
                await using var context = new CustomerManagementContext(options);
                await context.Database.EnsureCreatedAsync();
            }

            return new CustomerManagementContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection == null) return;
            _connection.Dispose();
            _connection = null;
        }
    }
}