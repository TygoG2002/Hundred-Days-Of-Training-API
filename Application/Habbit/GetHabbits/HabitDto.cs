using Domain.Entities.Habits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Habits.GetHabits
{
    using Domain.Entities.Habits;

  
        public class HabitDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;

            public HabitType Type { get; set; }

            public int? TargetValue { get; set; }

            public int TodayValue { get; set; }
            public bool TodayCompleted { get; set; }

            public bool IsValueBased => Type == HabitType.VALUE;
            public bool IsBinary => Type == HabitType.BINARY;
        }
    

}

