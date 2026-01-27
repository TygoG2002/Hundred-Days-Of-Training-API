using Application.interfaces;
using Application.Template.GetTemplates;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateQueryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public TemplateRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory; 
        }

        public async Task<List<WorkoutTemplateDto>> GetTemplatesAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
        SELECT
            t.Id AS TemplateId,
            t.Name AS TemplateName,
            t.Description,
            e.Name AS ExerciseName,
            e.Sets,
            e.Reps,
            e.RestSeconds
        FROM WorkoutTemplate t
        LEFT JOIN TemplateExercise e
            ON e.WorkoutTemplateId = t.Id
        ORDER BY t.Id;
        ";

            var rows = await connection.QueryAsync(sql);

            return rows
                .GroupBy(r => (int)r.TemplateId)
                .Select(g =>
                {
                    var first = g.First();

                    return new WorkoutTemplateDto
                    {
                        Id = (int)first.TemplateId,
                        Name = (string)first.TemplateName,
                        Description = (string)first.Description,
                        Exercises = g
                            .Where(x => x.ExerciseName != null)
                            .Select(x => new TemplateExerciseDto
                            {
                                Name = (string)x.ExerciseName,
                                Sets = (int)x.Sets,
                                Reps = (int)x.Reps,
                                RestSeconds = (int)x.RestSeconds
                            })
                            .ToList()
                    };
                })
                .ToList();
        }



    }
}
