using Domain.Entities.Habits;

namespace Application.Interfaces
{
    public interface IHabitCommandRepository
    {

        Task AddHabitValueAsync(int habitId, int amount);
        Task CompleteHabitAsync(int habitId);
    }
}
