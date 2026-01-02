using Application.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseMySql(
                        connectionString,
                        new MySqlServerVersion(new Version(11, 8, 3)), 
                        mySqlOptions =>
                        {
                            mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(5),
                                errorNumbersToAdd: null
                            );
                        }
                    ),
                ServiceLifetime.Scoped 
            );

            services.AddScoped<IWorkoutRepository, WorkoutRepository>();

            return services;
        }
    }
}
