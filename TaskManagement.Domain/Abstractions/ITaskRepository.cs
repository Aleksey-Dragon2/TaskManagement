using Task = System.Threading.Tasks.Task;
namespace TaskManagement.Domain.Abstractions;

public interface ITaskRepository
{
    Task<IEnumerable<TaskManagement.Domain.Entities.Task>> GetAllAsync();
    Task CreateAsync(TaskManagement.Domain.Entities.Task task);
    Task<bool> UpdateAsync(int id);
    Task<bool> DeleteAsync(int id);
}