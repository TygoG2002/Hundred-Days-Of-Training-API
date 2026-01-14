using Application.WorkoutSession.GetWorkoutInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface ISessionQueryRepository
    {
        Task<WorkoutSessionDetailsDto> GetSession(int sessionId);
    }

}
