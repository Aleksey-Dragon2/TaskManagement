namespace TaskManagement.Application.Abstractions;

public interface ITaskService
{
    Task<IEnumerable<TaskManagement.Domain.Entities.Task> > GetAllAsync();
    Task CreateAsync(TaskManagement.Domain.Entities.Task task);
    Task<bool> UpdateAsync(int id, bool isCompleted);
    Task<bool> DeleteAsync(int id);
}