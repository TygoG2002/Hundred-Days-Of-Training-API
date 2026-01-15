using Application.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.UpdateWeekPlanning
{
    public class UpdateWeekPlanningHandler : IRequestHandler<UpdateWeekPlanningCommand>
    {
        IDashboardCommandRepository _dashBoardRepository;

        public UpdateWeekPlanningHandler(IDashboardCommandRepository repo)
        {
            _dashBoardRepository = repo;
        }
        public async Task Handle(UpdateWeekPlanningCommand request, CancellationToken cancellationToken)
        {
            await _dashBoardRepository.UpdateWeekPlanningAsync(
     new UpdateWeekPlanningDto
     {
         Id = request.Request.Id,
         DayOfWeek = request.Request.DayOfWeek
     });

        }
    }
}
