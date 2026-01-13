using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Template.GetTemplates
{   
    public class GetTemplatesHandler : IRequestHandler<GetTemplatesQuery, List<WorkoutTemplateDto>>
    {
        private readonly ITemplateQueryRepository _templateQueryRepository;

        public GetTemplatesHandler(ITemplateQueryRepository repo)
        {
            _templateQueryRepository = repo; 
        }
        public async Task<List<WorkoutTemplateDto>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
        {
            return await _templateQueryRepository.GetTemplatesAsync();
        }
    }
}
