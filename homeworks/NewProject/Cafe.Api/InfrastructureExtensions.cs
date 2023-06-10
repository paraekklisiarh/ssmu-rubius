using Cafe.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebCafe;

public static class InfrastructureExtensions
{
    public static IServiceCollection RegisterDataBase(this IServiceCollection services)
    {
        var dbConnectionString = @"Server=.\SQLEXPRESS;Integrated Security=SSPI;Database=Hospital;TrustServerCertificate=True";
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var currentAssemblyName = typeof(AppDbContext).Assembly.FullName;
            options.UseSqlServer(
                dbConnectionString,
                b => b.MigrationsAssembly(currentAssemblyName));
        });

        return services;
    }
}