using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Plans.UpdateSet
{
    public record class UpdateSetCommand(int planId, int day, int index, bool completed) : IRequest
    {
    }
}
