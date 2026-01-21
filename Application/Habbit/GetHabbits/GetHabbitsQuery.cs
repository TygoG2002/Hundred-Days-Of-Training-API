using Application.Habbit.GetHabbits;
using MediatR;

namespace Application.Habits.GetHabits
{
    public record GetHabitsQuery : IRequest<List<HabitDto>>;
}
