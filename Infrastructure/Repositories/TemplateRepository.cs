using Application.interfaces;
using Application.Template.GetTemplates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateQueryRepository
    {
        private readonly AppDbContext _db;

        public TemplateRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<WorkoutTemplateDto>> GetTemplatesAsync()
        {
            return await _db.WorkoutTemplates
                .AsNoTracking()
                .Include(t => t.Exercises) // ✅ JUIST
                .Select(t => new WorkoutTemplateDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Exercises = t.Exercises.Select(e => new TemplateExerciseDto
                    {
                        Name = e.Name,
                        Sets = e.Sets,
                        Reps = e.Reps,
                        RestSeconds = e.RestSeconds
                    }).ToList()
                })
                .ToListAsync();
        }


    }
}
