using Application.Interfaces;
using MediatR;

namespace Application.Habits.UpdateValue
{
    public class UpdateHabitValueHandler : IRequestHandler<UpdateHabitValueCommand>
    {
        private readonly IHabitCommandRepository _repository;

        public UpdateHabitValueHandler(IHabitCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateHabitValueCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateValueAsync(
                request.HabitId,
                request.Amount);
        }
    }
}
