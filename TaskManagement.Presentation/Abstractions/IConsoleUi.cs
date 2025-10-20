namespace TaskManagement.Abstractions
{
    public interface IConsoleUi
    {
        public Task RunAsync();
        public Task ShowTasksAsync();
        public Task CreateTaskAsync();
        public Task UpdateTaskAsync();
        public Task DeleteTaskAsync();
    }
}
