using HundredDays.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Plans.GetSets
{
    public record GetSetsQuery(int PlanId, int Day)
        : IRequest<List<WorkoutSet>>;
}
