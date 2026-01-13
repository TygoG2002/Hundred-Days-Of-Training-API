using Application.Days.CompleteDay;
using Application.interfaces;
using MediatR;

public class CompleteDayHandler : IRequestHandler<CompleteDayCommand>
{
    private readonly IDayCommandRepository _repository;

    public CompleteDayHandler(IDayCommandRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        CompleteDayCommand request,
        CancellationToken cancellationToken)
    {
        await _repository.completeDay(request.planId, request.dayId, request.completed);

    }
}

