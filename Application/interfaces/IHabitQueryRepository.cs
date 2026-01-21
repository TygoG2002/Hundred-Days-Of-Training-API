using Application.Habits.GetHabits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IHabitQueryRepository
    {
        Task<List<HabitDto>> GetAllAsync();
    }
}
