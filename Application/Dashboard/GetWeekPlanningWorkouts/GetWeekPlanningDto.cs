using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.GetWeekPlanningWorkouts
{
    public class GetWeekPlanningDto
    {
        public int Id { get; set; }         
        public DayOfWeek DayOfWeek { get; set; }
        public int templateId { get; set; }
    }
}
