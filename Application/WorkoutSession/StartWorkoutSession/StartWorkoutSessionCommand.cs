using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.StartWorkoutSession
{
    public record StartWorkoutSessionCommand(int templateId) : IRequest<WorkoutSessionDto>;
}
