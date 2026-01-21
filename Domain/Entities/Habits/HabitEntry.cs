namespace Domain.Entities.Habits
{
    public class HabitEntry
    {
        public int Id { get; private set; }

        public int HabitId { get; private set; }
        public Habit Habit { get; private set; }

        public DateOnly Date { get; private set; }

        public int Value { get; private set; }
        public bool Completed { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        private HabitEntry() { }

        public HabitEntry(int habitId, DateOnly date)
        {
            HabitId = habitId;
            Date = date;
            Value = 0;
            Completed = false;
        }

        public void AddValue(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            Value += amount;
        }

        public void Complete()
        {
            Completed = true;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
