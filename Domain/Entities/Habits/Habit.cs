using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Habits
{
    public class Habit
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int TargetValue { get; private set; }


    }
}
