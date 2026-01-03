using Application.Days.Interfaces;
using MediatR;

namespace Application.Days.GetDayProgress;

public class GetDayProgressHandler
    : IRequestHandler<GetDayProgressQuery, GetDayProgressResult>
{
    private readonly IDayProgressRepository _repository;

    public GetDayProgressHandler(IDayProgressRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetDayProgressResult> Handle(GetDayProgressQuery request, CancellationToken cancellationToken)
    {
        var (done, total) = await _repository.GetDayProgressAsync(
           request.PlanId,
           request.Day);

        return new GetDayProgressResult(done, total);
    }
}
