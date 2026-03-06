using Infrastructure.Persistance.EFC.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extentions;

public static class InfrastructureServiceRegistrationExtentions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
        {
            // Using an in-memory SQLite database for development and testing purposes.
            services.AddSingleton(_ =>
            {
                var connectionString = "Data Source=:memory:;Cache=Shared";
                var conn = new SqliteConnection(connectionString);
                conn.Open();

                Console.WriteLine($"DEVELOPMENT: {connectionString}");

                return conn;
            });

            services.AddDbContext<DataContext>((sp, options) =>
            {
                var conn = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(conn);
            });

        }
        else
        {
            var connectionString = configuration.GetConnectionString("ProductionDatabase")
                ?? throw new InvalidOperationException("Connection string to Production Database not found.");

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            Console.WriteLine($"PRODUCTION: {connectionString}");
        }

        return services;
    }
}
