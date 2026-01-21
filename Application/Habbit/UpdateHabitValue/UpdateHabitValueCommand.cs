using MediatR;

namespace Application.Habits.UpdateValue
{
    public record UpdateHabitValueCommand(int HabitId, int Amount) : IRequest;
}
