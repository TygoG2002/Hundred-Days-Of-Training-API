using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    //
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<WorkoutDay> WorkoutDays => Set<WorkoutDay>();
    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }
}
