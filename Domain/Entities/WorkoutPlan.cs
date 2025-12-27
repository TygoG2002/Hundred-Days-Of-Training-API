namespace HundredDays.Domain.Entities;

public class WorkoutPlan
{
    private readonly List<WorkoutDay> _days = new();

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int TotalDays { get; private set; }

    public IReadOnlyCollection<WorkoutDay> Days => _days.AsReadOnly();

    private WorkoutPlan() { }

    public WorkoutPlan(string name, int totalDays)
    {
        Name = name;
        TotalDays = totalDays;
    }

    public WorkoutDay AddDay(int dayNumber)
    {
        if (dayNumber < 1 || dayNumber > TotalDays)
            throw new ArgumentOutOfRangeException(nameof(dayNumber));

        var day = new WorkoutDay(dayNumber);
        _days.Add(day);
        return day;
    }
}
