using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.UpdateWeekPlanning
{
    public class UpdateWeekPlanningDto
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
