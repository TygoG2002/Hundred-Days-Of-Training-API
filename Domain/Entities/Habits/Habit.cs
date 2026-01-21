namespace Domain.Entities.Habits
{
    public class Habit
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public HabitType Type { get; private set; }

        public int? TargetValue { get; private set; }

        private Habit() { } 

      

        public bool IsValueBased => Type == HabitType.VALUE;
        public bool IsBinary => Type == HabitType.BINARY;
    }
}
