using Application.Plans.Interfaces;
using HundredDays.Application.Plans.GetPlans;
using HundredDays.Domain.Entities;
using MediatR;
using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Plans.GetPlans
{
    public class GetPlansHandler : IRequestHandler<GetPlansQuery, List<WorkoutPlan>>
    {

        private readonly IPlanQueryRepository _PlanQueryRepository;

        public GetPlansHandler(IPlanQueryRepository repo)
        {
            _PlanQueryRepository = repo; 
        }
        public async Task<List<WorkoutPlan>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
           var result = await _PlanQueryRepository.GetAllAsync();
            return result; 
        }
    }
 
}
