using Application.interfaces;
using MediatR;

namespace Application.Days.GetDayProgress;

public class GetDayProgressHandler
    : IRequestHandler<GetDayProgressQuery, GetDayProgressResult>
{
    private readonly IWorkoutRepository _repository;

    public GetDayProgressHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetDayProgressResult> Handle(GetDayProgressQuery request, CancellationToken cancellationToken)
    {
        var (done, total) = await _repository.GetDayProgress(
           request.PlanId,
           request.Day);

        return new GetDayProgressResult(done, total);
    }
}
