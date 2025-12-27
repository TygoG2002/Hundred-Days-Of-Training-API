namespace HundredDays.Domain.Entities;

public class WorkoutSet
{
    public int Id { get; private set; }
    public int Reps { get; private set; }
    public bool Completed { get; private set; }

    public int WorkoutDayId { get; private set; }
    public WorkoutDay Day { get; private set; } = null!;

    private WorkoutSet() { }

    internal WorkoutSet(int reps)
    {
        Reps = reps;
        Completed = false;
    }

    public void MarkCompleted()
    {
        Completed = true;
    }

    public void MarkIncomplete()
    {
        Completed = false;
    }
}
