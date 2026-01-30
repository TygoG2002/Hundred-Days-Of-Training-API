using Application.interfaces;
using MediatR;

namespace Application.Dashboard.GetWeekProgress
{
    public class GetWeekProgressHandler : IRequestHandler<GetWeekProgressQuery, List<DayOfWeek>>
    {

        private readonly IDashboardQueryRepository _dashboardRepository;

        public GetWeekProgressHandler(IDashboardQueryRepository repo)
        {
            _dashboardRepository = repo; 
        }

        public async Task<List<DayOfWeek>> Handle(GetWeekProgressQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetWeekProgressAsync();
        }
    }
}
