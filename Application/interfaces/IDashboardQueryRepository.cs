using Application.Dashboard.GetTodaysWorkouts;
using Application.Plans.GetPlansWithProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IDashboardQueryRepository
    {
        Task<List<PlanOverviewDto>> GetTodayWorkoutsAsync();
        Task<List<TodayTemplateDto>> GetTodayTemplatesAsync();
    }


}
