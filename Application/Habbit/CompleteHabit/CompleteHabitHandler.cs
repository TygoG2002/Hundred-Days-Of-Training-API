using Application.Habbit.CompleteHabit;
using Application.Interfaces;
using MediatR;

namespace Application.Habits.CompleteHabit;

public sealed class CompleteHabitHandler : IRequestHandler<CompleteHabitCommand>
{
    private readonly IHabitCommandRepository _habitRepository;

    public CompleteHabitHandler(IHabitCommandRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task Handle(CompleteHabitCommand request, CancellationToken cancellationToken)
    {
        await _habitRepository.CompleteHabitAsync(request.habitId);
    }
}
