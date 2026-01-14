using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.GetWorkoutInfo
{
    public class GetWorkoutInfoHandler : IRequestHandler<GetWorkoutInfoQuery, WorkoutSessionDetailsDto>
    {
        private readonly ISessionQueryRepository _repo;

        public GetWorkoutInfoHandler(ISessionQueryRepository repo)
        {
            _repo = repo;
        }

        public Task<WorkoutSessionDetailsDto> Handle(GetWorkoutInfoQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetSession(request.SessionId);
        }
    }

}
