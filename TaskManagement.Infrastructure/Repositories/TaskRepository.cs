using Dapper;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Infrastructure.Abstractions;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _connection;
    
    public TaskRepository(IDbConnectionFactory connection)
    {
      _connection = connection;
    }

    public async Task<IEnumerable<TaskManagement.Domain.Entities.Task>> GetAllAsync()
    {
        using var connection = await _connection.CreateDbConnectionAsync();
        var tasks = await connection.QueryAsync<TaskManagement.Domain.Entities.Task>("SELECT * FROM Tasks");
        return tasks;
    }
    
    public async Task CreateAsync(TaskManagement.Domain.Entities.Task task)
    {
        using var connection = await _connection.CreateDbConnectionAsync();
        await connection.ExecuteAsync("INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)" +
                                      "VALUES (@Title, @Description, @IsCompleted, @CreatedAt)", task);
    }

    public async Task<bool> UpdateAsync(int id)
    {
       using var connection = await _connection.CreateDbConnectionAsync();
       var result = await connection.ExecuteAsync("UPDATE Tasks SET IsCompleted = NOT IsCompleted WHERE Id = @Id",
           new { Id = id });
       return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = await _connection.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync("DELETE FROM TASKS WHERE Id = @Id", new { Id = id });
        return result > 0;
    } 
}