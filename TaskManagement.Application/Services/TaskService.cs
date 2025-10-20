using TaskManagement.Application.Abstractions;
using TaskManagement.Domain.Abstractions;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository  _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<IEnumerable<TaskManagement.Domain.Entities.Task>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        return tasks;
    }

    public async Task CreateAsync(TaskManagement.Domain.Entities.Task task)
    {
        await _taskRepository.CreateAsync(task);
    }

    public async Task<bool> UpdateAsync(int id) 
    {
       var result = await  _taskRepository.UpdateAsync(id);
        return result; 
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await  _taskRepository.DeleteAsync(id);
        return result;
    }
}