using Domain.Entities.Template;
using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<WorkoutDay> WorkoutDays => Set<WorkoutDay>();
    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();

    public DbSet<WorkoutTemplate> WorkoutTemplates => Set<WorkoutTemplate>();
    public DbSet<TemplateExercise> TemplateExercises => Set<TemplateExercise>();
    public DbSet<WorkoutTemplateScheduledDay> WorkoutTemplateScheduledDays => Set<WorkoutTemplateScheduledDay>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------------------------
        // WorkoutTemplate
        // -------------------------
        modelBuilder.Entity<WorkoutTemplate>(b =>
        {
            b.ToTable("WorkoutTemplate");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Description);

            // Exercises (backing field: _exercises)
            b.HasMany(x => x.Exercises)
                .WithOne()
                .HasForeignKey("WorkoutTemplateId")
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Exercises)
                .HasField("_exercises")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // ScheduledDays (backing field: _scheduledDays)
            b.HasMany(x => x.ScheduledDays)
                .WithOne()
                .HasForeignKey("WorkoutTemplateId")
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.ScheduledDays)
                .HasField("_scheduledDays")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        // -------------------------
        // TemplateExercise
        // -------------------------
        modelBuilder.Entity<TemplateExercise>(b =>
        {
            b.ToTable("TemplateExercise");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Sets).IsRequired();
            b.Property(x => x.Reps).IsRequired();
            b.Property(x => x.RestSeconds).IsRequired();
        });

        // -------------------------
        // WorkoutTemplateScheduledDay
        // -------------------------
        modelBuilder.Entity<WorkoutTemplateScheduledDay>(b =>
        {
            b.ToTable("WorkoutTemplateScheduledDay");
            b.HasKey(x => x.Id);

            b.Property(x => x.DayOfWeek)
                .IsRequired()
                .HasConversion<int>();
        });
    }
}
