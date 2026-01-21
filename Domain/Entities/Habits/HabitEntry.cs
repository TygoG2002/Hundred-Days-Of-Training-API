namespace Domain.Entities.Habits
{
    public class HabitEntry
    {
        public int Id { get; private set; }

        public bool Completed { get; private set; }

        public int Value { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public int HabitId { get; private set; }
        public Habit Habit { get; private set; }

        private HabitEntry() { }

        public HabitEntry(int habitId, int value)
        {
            HabitId = habitId;
            Completed = false;
            Value = value;
        }

        public void Complete()
        {
            Completed = true;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
