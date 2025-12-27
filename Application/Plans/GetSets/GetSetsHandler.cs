using Application.interfaces;
using HundredDays.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Plans.GetSets
{
    public class GetSetsHandler : IRequestHandler<GetSetsQuery, List<WorkoutSet>>
    {
        private readonly IWorkoutRepository _workoutRepository;

        public GetSetsHandler(IWorkoutRepository repo)
        {
            _workoutRepository = repo;
        }

        public async Task<List<WorkoutSet>> Handle(GetSetsQuery request, CancellationToken cancellationToken)
        {
            return await _workoutRepository.GetSets(request.PlanId, request.Day);
        }
    }
}
