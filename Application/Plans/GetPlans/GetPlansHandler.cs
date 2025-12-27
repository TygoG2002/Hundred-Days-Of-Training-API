using Application.interfaces;
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

        private readonly IWorkoutRepository _workoutRepository;

        public GetPlansHandler(IWorkoutRepository repo)
        {
            _workoutRepository = repo; 
        }
        public async Task<List<WorkoutPlan>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
           var result = await _workoutRepository.GetAllAsync();
            return result; 
        }
    }
 
}
