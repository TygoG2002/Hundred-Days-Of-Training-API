using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Template.GetTemplates
{

        public record GetTemplatesQuery : IRequest<List<WorkoutTemplateDto>>;
    
}
