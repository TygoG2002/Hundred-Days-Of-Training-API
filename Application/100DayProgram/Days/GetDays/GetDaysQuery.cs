using HundredDays.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Days.GetDays
{
    public record GetDaysQuery(int planId) : IRequest<List<DayOverviewDto>>;

}
