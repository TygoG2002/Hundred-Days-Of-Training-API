using Application.Plans.GetPlansWithProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.GetTodaysWorkouts;

public class TodayDashboardDto
{
    public List<PlanOverviewDto> Plans { get; set; } = [];
    public List<TodayTemplateDto> Templates { get; set; } = [];
}
