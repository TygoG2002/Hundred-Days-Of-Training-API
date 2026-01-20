using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.DeleteWorkoutSession
{
    public record DeleteWorkoutSessionCommand(int sessionId) : IRequest;
   
}
