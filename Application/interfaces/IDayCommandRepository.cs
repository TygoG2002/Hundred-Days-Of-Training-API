using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IDayCommandRepository
    {
        Task CompleteDayAsync(int planId, int dayId, bool completed); 
    }
}
