using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TaskManagement.Abstractions;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Services;

namespace TaskManagement.Implementation
{ 
    class ConsoleUi : IConsoleUi 
    {
        private readonly ITaskService _taskService;

        public ConsoleUi(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("1. Show all tasks");
                    Console.WriteLine("2. Create a new task");
                    Console.WriteLine("3. Update status a task");
                    Console.WriteLine("4. Delete a task");
                    Console.WriteLine("0. Exit ");
                    Console.Write("Input:");

                    var choise = Console.ReadLine()?.Trim() ?? "";
                    Console.WriteLine();
                    switch (choise)
                    {
                        case "1":
                            await ShowTasksAsync(); break;
                        case "2":
                            await CreateTaskAsync(); break;
                        case "3":
                            await UpdateTaskAsync(); break;
                        case "4":
                            await DeleteTaskAsync(); break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid operation");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task ShowTasksAsync()
        {
            var tasks = await _taskService.GetAllAsync();
            if (tasks.Count() == 0)
                Console.WriteLine("No tasks");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Id: {task.Id}\nTitle: {task.Title}\nDescription: {task.Description}" +
                    $"\nCompleted: {task.IsCompleted}\nDate of creation: {task.CreatedAt}");
            }
            Console.WriteLine();
        }

        public async Task CreateTaskAsync()
        {
            string title;
            while (true)
            {
                Console.Write("Enter task title:");
                title = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(title))
                    break;
                Console.WriteLine("Please try again. Title cannot be empty");
            }

            string description;
            while (true)
            {
                Console.Write("Enter task description:");
                description = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(description))
                    break;
                Console.WriteLine("Please try again. Description cannot be empty");
            }

            var task = new TaskManagement.Domain.Entities.Task(title, description);
            await _taskService.CreateAsync(task);
            Console.WriteLine("Task Added");
            Console.WriteLine();
        }

        public async Task UpdateTaskAsync()
        {
            int id;
            while (true)
            {
                Console.Write("Enter task id:");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                Console.WriteLine("Id not found. Please try again");
            }

            var result = await _taskService.UpdateAsync(id);
            if (result)
                Console.WriteLine("Task has been updated");
            else
                Console.WriteLine("Task has not been updated");

            Console.WriteLine();
        }

        public async Task DeleteTaskAsync()
        {
            while (true)
            {
                Console.Write("Enter task id:");
                if (int.TryParse(Console.ReadLine(), out var id))
                {
                    await _taskService.DeleteAsync(id);
                    Console.WriteLine("Task deleted");
                    Console.WriteLine();
                    return;
                }
                Console.WriteLine("Task not found. Please try again.");
            }
        }
    }
}
