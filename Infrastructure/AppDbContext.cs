using Domain.Entities.Template;
using Domain.Entities.WorkoutSession;
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

    public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
    public DbSet<WorkoutSessionExercise> WorkoutSessionExercises => Set<WorkoutSessionExercise>();
    public DbSet<WorkoutSessionSet> WorkoutSessionSets => Set<WorkoutSessionSet>();

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

            b.HasMany(x => x.Exercises)
                .WithOne()
                .HasForeignKey("WorkoutTemplateId")
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Exercises)
                .HasField("_exercises")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            b.HasMany(x => x.ScheduledDays)
                .WithOne()
                .HasForeignKey("WorkoutTemplateId")
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.ScheduledDays)
                .HasField("_scheduledDays")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<TemplateExercise>(b =>
        {
            b.ToTable("TemplateExercise");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Sets).IsRequired();
            b.Property(x => x.Reps).IsRequired();
            b.Property(x => x.RestSeconds).IsRequired();
        });

        modelBuilder.Entity<WorkoutTemplateScheduledDay>(b =>
        {
            b.ToTable("WorkoutTemplateScheduledDay");
            b.HasKey(x => x.Id);

            b.Property(x => x.DayOfWeek)
                .IsRequired()
                .HasConversion<int>();
        });

        // -------------------------
        // WorkoutSession (AGGREGATE ROOT)
        // -------------------------
        modelBuilder.Entity<WorkoutSession>(b =>
        {
            b.ToTable("WorkoutSession");
            b.HasKey(x => x.Id);

            b.Property(x => x.StartedAt).IsRequired();
            b.Property(x => x.FinishedAt);

            b.HasOne(x => x.WorkoutTemplate)
                .WithMany()
                .HasForeignKey(x => x.WorkoutTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(x => x.Exercises)
                .WithOne()
                .HasForeignKey(x => x.WorkoutSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Exercises)
                .HasField("_exercises")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        // -------------------------
        // WorkoutSessionExercise
        // -------------------------
        modelBuilder.Entity<WorkoutSessionExercise>(b =>
        {
            b.ToTable("WorkoutSessionExercise");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.TargetSets).IsRequired();
            b.Property(x => x.TargetReps).IsRequired();
            b.Property(x => x.RestSeconds).IsRequired();

            b.HasMany(x => x.Sets)
                .WithOne()
                .HasForeignKey(x => x.WorkoutSessionExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Sets)
                .HasField("_sets")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        // -------------------------
        // WorkoutSessionSet
        // -------------------------
        modelBuilder.Entity<WorkoutSessionSet>(b =>
        {
            b.ToTable("WorkoutSessionSet");
            b.HasKey(x => x.Id);

            b.Property(x => x.SetNumber).IsRequired();
            b.Property(x => x.Reps);
            b.Property(x => x.WeightKg);
        });
    }
}
