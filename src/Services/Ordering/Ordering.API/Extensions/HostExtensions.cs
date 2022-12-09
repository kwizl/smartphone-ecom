using System;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Persistence;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        // Migration method for automatically creating database when the docker application is started
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : OrderContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Throws exception if the service is not found, that the logger may log the error
                var logger = services.GetRequiredService<ILogger<TContext>>();

                // Returns null if the service is not found
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database association with context {dbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrating database association with context {dbContextName}", typeof(TContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }
            }
            return host;
        }

        // Migrates and Seeds data to the database
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
            IServiceProvider services) where TContext : OrderContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}