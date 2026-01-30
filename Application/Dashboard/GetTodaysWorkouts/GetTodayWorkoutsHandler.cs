using Application.interfaces;
using MediatR;

namespace Application.Dashboard.GetTodaysWorkouts
{
    public class GetTodayWorkoutsHandler : IRequestHandler<GetTodayWorkoutsQuery, TodayDashboardDto>
    {
        private readonly IDashboardQueryRepository _dashboardRepository;

        public GetTodayWorkoutsHandler(IDashboardQueryRepository repo)
        {
            _dashboardRepository = repo;
        }

        public async Task<TodayDashboardDto> Handle(GetTodayWorkoutsQuery request, CancellationToken cancellationToken)
        {
            var plans = await _dashboardRepository.GetTodayWorkoutsAsync();
            var templates = await _dashboardRepository.GetTodayTemplatesAsync();

            return new TodayDashboardDto
            {
                Plans = plans,
                Templates = templates
            };
        }
    }
}
