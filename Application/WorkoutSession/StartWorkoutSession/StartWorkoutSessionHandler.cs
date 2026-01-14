using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.StartWorkoutSession
{
    public class StartWorkoutSessionHandler : IRequestHandler<StartWorkoutSessionCommand, WorkoutSessionDto>
    {
        private readonly ISessionCommandRepository _sessionRepository;


        public StartWorkoutSessionHandler(ISessionCommandRepository repo)
        {
            _sessionRepository = repo; 
        }

        public async Task<WorkoutSessionDto> Handle(StartWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            return await _sessionRepository.StartWorkoutSessionAsync(request.templateId);
        }
    }
}
