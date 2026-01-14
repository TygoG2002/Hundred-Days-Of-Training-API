using Application.interfaces;
using Application.WorkoutSession.GetWorkoutInfo;
using Application.WorkoutSession.StartWorkoutSession;
using Domain.Entities.WorkoutSession;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SessionRepository : ISessionCommandRepository, ISessionQueryRepository
    {
        private readonly AppDbContext _db;

        public SessionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<WorkoutSessionDetailsDto> GetSession(int sessionId)
        {
            var session = await _db.WorkoutSessions
                .Include(s => s.Exercises)
                    .ThenInclude(e => e.Sets)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                throw new InvalidOperationException("WorkoutSession not found");

            var previousSession = await _db.WorkoutSessions
                .Where(s =>
                    s.WorkoutTemplateId == session.WorkoutTemplateId &&
                    s.FinishedAt != null &&
                    s.Id != session.Id)
                .OrderByDescending(s => s.FinishedAt)
                .Include(s => s.Exercises)
                    .ThenInclude(e => e.Sets)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new WorkoutSessionDetailsDto
            {
                SessionId = session.Id,
                WorkoutTemplateId = session.WorkoutTemplateId,

                StartedAt = session.StartedAt,

                Exercises = session.Exercises.Select(se =>
                {
                    var prevExercise = previousSession?
                        .Exercises
                        .FirstOrDefault(e => e.Name == se.Name);

                    return new WorkoutSessionExerciseDto
                    {
                        TemplateExerciseId = se.Id,
                        Name = se.Name,
                        Sets = se.TargetSets,
                        TargetReps = se.TargetReps,

                        SetsData = se.Sets
                            .OrderBy(s => s.SetNumber)
                            .Select(s =>
                            {
                                var prevSet = prevExercise?
                                    .Sets
                                    .FirstOrDefault(ps => ps.SetNumber == s.SetNumber);

                                return new WorkoutSessionSetDto
                                {
                                    SetNumber = s.SetNumber,

                                    LastReps = prevSet?.Reps,
                                    LastWeight = prevSet?.WeightKg,

                                    CurrentReps = s.Reps,
                                    CurrentWeight = s.WeightKg
                                };
                            })
                            .ToList()
                    };
                }).ToList()
            };
        }






        public async Task<WorkoutSessionDto> StartWorkoutSessionAsync(int templateId)
        {
            var template = await _db.WorkoutTemplates
                .Include(t => t.Exercises)
                .FirstOrDefaultAsync(t => t.Id == templateId);

            if (template == null)
                throw new InvalidOperationException("WorkoutTemplate not found");

            var session = new WorkoutSession(templateId);

            _db.WorkoutSessions.Add(session);
            await _db.SaveChangesAsync();

            foreach (var te in template.Exercises)
            {
                var sessionExercise = new WorkoutSessionExercise(
                    session.Id,
                    te.Name,
                    te.Sets,
                    te.Reps,
                    te.RestSeconds
                );

                for (int i = 1; i <= te.Sets; i++)
                    sessionExercise.AddSet(i, session.Id);

                _db.WorkoutSessionExercises.Add(sessionExercise);
            }

            await _db.SaveChangesAsync();

            return new WorkoutSessionDto
            {
                SessionId = session.Id,
                StartedAt = session.StartedAt
            };
        }

    }
}
