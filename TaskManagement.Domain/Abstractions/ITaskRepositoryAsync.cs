using Task = System.Threading.Tasks.Task;
namespace TaskManagement.Domain.Abstractions;

public interface ITaskRepositoryAsync
{
    Task CreateAsync(TaskManagement.Domain.Entities.Task task);
    Task<IEnumerable<TaskManagement.Domain.Entities.Task>> GetAllAsync();
    Task UpdateAsync(int id, bool completed);
    Task DeleteAsync(int id);
}