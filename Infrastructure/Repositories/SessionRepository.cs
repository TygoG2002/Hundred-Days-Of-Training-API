using Application.interfaces;
using Application.WorkoutSession.FinishWorkoutSession;
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

      public async Task SaveWorkoutSessionAsync(int sessionId, List<FinishWorkoutSessionExerciseDto> exercises)
        {
            var session = await _db.WorkoutSessions
                .Include(s => s.Exercises)
                    .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                throw new InvalidOperationException("WorkoutSession not found");

            foreach (var exerciseDto in exercises)
            {
                var sessionExercise = session.Exercises
                    .FirstOrDefault(e => e.Id == exerciseDto.WorkoutSessionExerciseId);

                if (sessionExercise == null)
                    continue;

                foreach (var setDto in exerciseDto.Sets)
                {
                    var set = sessionExercise.Sets
                        .FirstOrDefault(s => s.SetNumber == setDto.SetNumber);

                    if (set == null)
                        continue;

                    set.Update(
                        setDto.Reps,
                        setDto.WeightKg
                    );
                }
            }

            session.Finish();

            await _db.SaveChangesAsync();
        }


        public async Task<WorkoutSessionDto> StartWorkoutSessionAsync(int templateId)
        {
            var existing = await _db.WorkoutSessions
                .Include(s => s.Exercises)
                    .ThenInclude(e => e.Sets)
                .Where(s =>
                    s.WorkoutTemplateId == templateId &&
                    s.FinishedAt == null)
                .OrderByDescending(s => s.StartedAt)
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                if (!existing.Exercises.Any())
                {
                    var template = await _db.WorkoutTemplates
                        .Include(t => t.Exercises)
                        .FirstAsync(t => t.Id == templateId);

                    foreach (var te in template.Exercises)
                    {
                        var sessionExercise = new WorkoutSessionExercise(
                            existing.Id,
                            te.Name,
                            te.Sets,
                            te.Reps,
                            te.RestSeconds
                        );

                        for (int i = 1; i <= te.Sets; i++)
                            sessionExercise.AddSet(i, existing.Id);

                        _db.WorkoutSessionExercises.Add(sessionExercise);
                    }

                    await _db.SaveChangesAsync();
                }

                return new WorkoutSessionDto
                {
                    SessionId = existing.Id,
                    StartedAt = existing.StartedAt
                };
            }

            var newSession = new WorkoutSession(templateId);
            _db.WorkoutSessions.Add(newSession);
            await _db.SaveChangesAsync();

            var tpl = await _db.WorkoutTemplates
                .Include(t => t.Exercises)
                .FirstAsync(t => t.Id == templateId);

            foreach (var te in tpl.Exercises)
            {
                var sessionExercise = new WorkoutSessionExercise(
                    newSession.Id,
                    te.Name,
                    te.Sets,
                    te.Reps,
                    te.RestSeconds
                );

                for (int i = 1; i <= te.Sets; i++)
                    sessionExercise.AddSet(i, newSession.Id);

                _db.WorkoutSessionExercises.Add(sessionExercise);
            }

            await _db.SaveChangesAsync();

            return new WorkoutSessionDto
            {
                SessionId = newSession.Id,
                StartedAt = newSession.StartedAt
            };
        }


    }
}
