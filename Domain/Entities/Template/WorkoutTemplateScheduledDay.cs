using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Template
{
    public class WorkoutTemplateScheduledDay
    {
        public int Id { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }

        public int WorkoutTemplateId { get; private set; }
        public WorkoutTemplate WorkoutTemplate { get; private set; } = null!;

        private WorkoutTemplateScheduledDay() { }

        public WorkoutTemplateScheduledDay(
            DayOfWeek dayOfWeek,
            int workoutTemplateId)
        {
            DayOfWeek = dayOfWeek;
            WorkoutTemplateId = workoutTemplateId;
        }
    }

}

