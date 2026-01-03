using Application.Days.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Days.GetDays
{
    public class GetDaysHandler
       : IRequestHandler<GetDaysQuery, List<int>>
    {
        private readonly IDayQueryRepository _DayQueryRepository;


        public GetDaysHandler(IDayQueryRepository repo)
        {
            _DayQueryRepository = repo;
        }

        public async Task<List<int>> Handle(GetDaysQuery request, CancellationToken cancellationToken)
        {
            return await _DayQueryRepository.GetDaysAsync(request.planId);

        }
    }
}
