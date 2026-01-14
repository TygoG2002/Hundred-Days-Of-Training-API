using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.FinishWorkoutSession
{
    public class FinishWorkoutSessionHandler : IRequestHandler<FinishWorkoutSessionCommand>
    {
        private readonly ISessionCommandRepository _sessionCommandRepo;


        public FinishWorkoutSessionHandler(ISessionCommandRepository repo)
        {
            _sessionCommandRepo = repo; 
        }
        public async Task Handle(FinishWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
           await _sessionCommandRepo.SaveWorkoutSessionAsync(request.SessionId, request.Exercises);
        }
    }
}   
