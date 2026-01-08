namespace HundredDays.Domain.Entities;

public class WorkoutDay
{
    private readonly List<WorkoutSet> _sets = new();

    public int Id { get; private set; }
    public int DayNumber { get; private set; }

    public int WorkoutPlanId { get; private set; }
    public WorkoutPlan Plan { get; private set; } = null!;
    public DateTime? CompletedAt { get; private set; }

    public bool Completed { get; private set; }

    public IReadOnlyCollection<WorkoutSet> Sets => _sets.AsReadOnly();

    private WorkoutDay() { } 
    internal WorkoutDay(int dayNumber)
    {
        DayNumber = dayNumber;
    }

    public void AddSet(int reps)
    {
        if (reps <= 0)
            throw new ArgumentOutOfRangeException(nameof(reps));

        _sets.Add(new WorkoutSet(reps));
    }


    public static bool CompleteCurrentWorkout(WorkoutDay day)
    {
        return day.Completed = true;
    }
}
