using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Plans.UpdateSet
{
    public class UpdateSetHandler : IRequestHandler<UpdateSetCommand>
    {
        private readonly IWorkoutRepository _workoutRepository;

        public UpdateSetHandler(IWorkoutRepository repo)
        {
            _workoutRepository = repo; 
        }


        public async Task Handle(UpdateSetCommand request, CancellationToken cancellationToken)
        {
             await _workoutRepository.UpdateSet(request.setId, request.completed);
           
        }
    }
   
}
