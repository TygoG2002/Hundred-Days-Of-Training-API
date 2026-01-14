using Application.interfaces;
using Application.WorkoutSession.StartWorkoutSession;
using Domain.Entities.WorkoutSession;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SessionRepository : ISessionCommandRepository
    {
        private readonly AppDbContext _db;

        public SessionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<WorkoutSessionDto> StartWorkoutSessionAsync(int templateId)
        {
            var templateExists = await _db.WorkoutTemplates
                .AnyAsync(t => t.Id == templateId);

            if (!templateExists)
                throw new InvalidOperationException("WorkoutTemplate not found");

            var session = new WorkoutSession(templateId);

            _db.WorkoutSessions.Add(session);
            await _db.SaveChangesAsync();

            return new WorkoutSessionDto
            {
                SessionId = session.Id,
                StartedAt = session.StartedAt
            };
        }
    }
}
