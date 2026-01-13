using Domain.Entities.Template;
using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<WorkoutDay> WorkoutDays => Set<WorkoutDay>();
    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();

    public DbSet<WorkoutTemplate> WorkoutTemplate => Set<WorkoutTemplate>();

    public DbSet<TemplateExercise> TemplateExercise => Set<TemplateExercise>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }
}
