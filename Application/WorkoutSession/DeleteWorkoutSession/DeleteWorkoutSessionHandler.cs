using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.DeleteWorkoutSession
{
    public class DeleteWorkoutSessionHandler : IRequestHandler<DeleteWorkoutSessionCommand>
    {
        private readonly ISessionCommandRepository _sessionRepository;

        public DeleteWorkoutSessionHandler(ISessionCommandRepository repo)
        {
            _sessionRepository = repo;
        }

        public async Task Handle(DeleteWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            await _sessionRepository.DeleteWorkoutSessionAsync(request.sessionId);
        }
    }
}
