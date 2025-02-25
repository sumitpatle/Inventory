using Application.Common.Interface;
using Ardalis.GuardClauses;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppDbConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'AppDbConnection' not found.");

            //Configure DbContext to use SQLitein app db for local development

            services.AddDbContext<BookingContext>((sp, options) =>
            {
                options.UseSqlite(connectionString); // Adjust the path if necessary
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            }, ServiceLifetime.Scoped);

            services.AddScoped<IBookingDbContext>(provider => provider.GetRequiredService<BookingContext>());
            services.AddScoped<IUploadService, UploadService>();
            return services;

        }
    }
}
