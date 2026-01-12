using Application.interfaces;
using Application.Plans.GetPlansWithProgress;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.GetTodaysWorkouts
{
    public class GetTodayWorkoutsHandler : IRequestHandler<GetTodayWorkoutsQuery, List<PlanOverviewDto>>
    {

        private readonly IDashboardQueryRepository _dashboardRepository;

        public GetTodayWorkoutsHandler(IDashboardQueryRepository repo)
        {
            _dashboardRepository = repo; 
        }

        public async Task<List<PlanOverviewDto>> Handle(GetTodayWorkoutsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetTodayWorkouts();
        }
    }
}
