using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Days.CompleteDay
{
    public record CompleteDayRequest(int planId, int dayId, bool completed)
    {
    }
}
