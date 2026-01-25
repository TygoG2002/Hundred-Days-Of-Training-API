namespace Application.Plans.GetPlansWithProgress;

public class PlanOverviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int CompletedDays { get; set; }
    public int TotalDays { get; set; }
}
