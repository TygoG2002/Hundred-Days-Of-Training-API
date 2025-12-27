using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Plans.GetDays
{
    public class GetDaysHandler
       : IRequestHandler<GetDaysQuery, List<int>>
    {
        private readonly IWorkoutRepository _workoutRepository;


        public GetDaysHandler(IWorkoutRepository repo)
        {
            _workoutRepository = repo;
        }

        public async Task<List<int>> Handle(GetDaysQuery request, CancellationToken cancellationToken)
        {
            return await _workoutRepository.GetDays(request.planId);

        }
    }
}
