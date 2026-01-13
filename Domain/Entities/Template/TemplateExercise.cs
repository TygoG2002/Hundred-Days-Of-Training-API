using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Template
{
    public class TemplateExercise
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = "";
        public int Sets { get; private set; }
        public int Reps { get; private set; }
        public int RestSeconds { get; private set; }

        //for EF
        private TemplateExercise() { } 

        public TemplateExercise(string name, int sets, int reps, int restSeconds)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
            RestSeconds = restSeconds;
        }
    }

}
