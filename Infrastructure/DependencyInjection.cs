using Application.Days.Interfaces;
using Application.interfaces;
using Application.Interfaces;
using Application.Plans.Interfaces;
using Application.Sets.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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


            services.AddScoped<IPlanQueryRepository, WorkoutPlanRepository>();
            services.AddScoped<IDayQueryRepository, WorkoutDayRepository>();
            services.AddScoped<ISetQueryRepository, WorkoutSetRepository>();
            services.AddScoped<IDayCommandRepository, WorkoutDayRepository>();
            services.AddScoped<IDashboardQueryRepository, DashboardRepository>();
            services.AddScoped<IDashboardCommandRepository, DashboardRepository>();
            services.AddScoped<ITemplateQueryRepository, TemplateRepository>();
            services.AddScoped<ISessionCommandRepository, SessionRepository>();
            services.AddScoped<ISessionQueryRepository, SessionRepository>();
            services.AddScoped<IHabitQueryRepository, HabitRepository>();
            services.AddScoped<IHabitCommandRepository, HabitRepository>();

            services.AddScoped<IDbConnectionFactory>(sp =>
                new SqlConnectionFactory(connectionString)
            );


            return services;
        }
    }
}
