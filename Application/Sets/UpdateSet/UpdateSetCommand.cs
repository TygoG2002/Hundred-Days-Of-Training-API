using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sets.UpdateSet
{
    public record UpdateSetCommand(int setId, bool completed) : IRequest
    {
    }
}
