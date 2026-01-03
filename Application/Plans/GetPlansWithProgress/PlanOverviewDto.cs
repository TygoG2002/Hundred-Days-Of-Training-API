namespace Application.Plans.GetPlansWithProgress;

public record PlanOverviewDto(
    int Id,
    string Name,
    int CompletedDays,
    int TotalDays
);
