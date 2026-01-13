using Application.Sets.Interfaces;
using HundredDays.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sets.GetSets
{
    public class GetSetsHandler : IRequestHandler<GetSetsQuery, List<WorkoutSet>>
    {
        private readonly ISetQueryRepository _SetQueryRepository;

        public GetSetsHandler(ISetQueryRepository repo)
        {
            _SetQueryRepository = repo;
        }

        public async Task<List<WorkoutSet>> Handle(GetSetsQuery request, CancellationToken cancellationToken)
        {
            return await _SetQueryRepository.GetSetsAsync(request.PlanId, request.Day);
        }
    }
}
