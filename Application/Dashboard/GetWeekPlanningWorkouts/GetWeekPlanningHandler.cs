using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.GetWeekPlanningWorkouts
{

    
    public class GetWeekPlanningHandler : IRequestHandler<GetWeekPlanningQuery, List<GetWeekPlanningDto>>
    {
        IDashboardQueryRepository _dashboardRepository;


        public GetWeekPlanningHandler(IDashboardQueryRepository repo)
        {
            _dashboardRepository = repo;
        }
        public async Task<List<GetWeekPlanningDto>> Handle(GetWeekPlanningQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetWeekPlanningAsync();
        }
    }
}
