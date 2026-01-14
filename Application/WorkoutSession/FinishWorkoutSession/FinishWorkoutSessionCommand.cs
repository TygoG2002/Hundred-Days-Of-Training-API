using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.FinishWorkoutSession
{
    public record FinishWorkoutSessionCommand(int SessionId, List<FinishWorkoutSessionExerciseDto> Exercises) : IRequest;

}
