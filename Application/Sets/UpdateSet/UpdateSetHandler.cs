using Application.Sets.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sets.UpdateSet
{
    public class UpdateSetHandler : IRequestHandler<UpdateSetCommand>
    {
        private readonly ISetCommandRepository _SetCommandRepository;

        public UpdateSetHandler(ISetCommandRepository repo)
        {
            _SetCommandRepository = repo; 
        }


        public async Task Handle(UpdateSetCommand request, CancellationToken cancellationToken)
        {
             await _SetCommandRepository.UpdateSetAsync(request.setId, request.completed);
           
        }
    }
   
}
