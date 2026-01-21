using Domain.Entities.Habits;

namespace Application.Interfaces
{
    public interface IHabitCommandRepository
    {

        Task UpdateValueAsync(int habitId, int amount);

    }
}
