namespace Application.Sets.Interfaces;

public interface ISetCommandRepository
{
    Task UpdateSetAsync(int setId, bool completed);
}
