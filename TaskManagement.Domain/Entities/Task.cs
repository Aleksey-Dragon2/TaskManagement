namespace TaskManagement.Domain.Entities;

public class Task
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public Task(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty");
        
        Title = title;
        Description = description;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public Task()
    {

    }
}