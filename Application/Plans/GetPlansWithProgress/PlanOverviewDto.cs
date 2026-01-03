namespace Application.Plans.GetPlansWithProgress;

public record PlanOverviewDto(
    int Id,
    string Name,
    string Description,
    int CompletedDays,
    int TotalDays
);
