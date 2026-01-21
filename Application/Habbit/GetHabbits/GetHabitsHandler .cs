using Application.Habbit.GetHabbits;
using Application.Habits.GetHabits;
using Application.interfaces;
using MediatR;

public class GetHabitsHandler : IRequestHandler<GetHabitsQuery, List<HabitDto>>
{
    private readonly IHabitQueryRepository _repo;

    public GetHabitsHandler(IHabitQueryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<HabitDto>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}
