using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Threading;

namespace Entity.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var connection = this.Database.GetDbConnection();
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(text, parameters, commandType: type, commandTimeout: timeout);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var connection = this.Database.GetDbConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<T>(text, parameters, commandType: type, commandTimeout: timeout);
        }
        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
        }

        public readonly struct DapperEFCoreCommand : IDisposable
        {
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct
                );
            }

            public CommandDefinition Definition { get; }

            public void Dispose()
            {
            }
        }
    }
}
