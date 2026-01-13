using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Template
{
    public class WorkoutTemplate
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = "";
        public string? Description { get; private set; }


        private readonly List<WorkoutTemplateScheduledDay> _scheduledDays = new();
        public IReadOnlyCollection<WorkoutTemplateScheduledDay> ScheduledDays => _scheduledDays;


        private readonly List<TemplateExercise> _exercises = new();
        public IReadOnlyCollection<TemplateExercise> Exercises => _exercises;

        private WorkoutTemplate() { } 

        public WorkoutTemplate(string name, int estimatedDurationMinutes, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public void AddExercise(string name, int sets, int reps, int restSeconds)
        {
            _exercises.Add(new TemplateExercise(
                name, sets, reps, restSeconds));
        }
    }

}
