using MediatR;

namespace Application.Habits.GetHabits
{
    public record GetHabitsQuery : IRequest<List<HabitDto>>;
}
