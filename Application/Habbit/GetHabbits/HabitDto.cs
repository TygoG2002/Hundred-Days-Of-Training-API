using Domain.Entities.Habits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Habits.GetHabits
{
    public class HabitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TargetValue { get; set; }

        public int TodayValue { get; set; }
        public bool TodayCompleted { get; set; }
    }
}

