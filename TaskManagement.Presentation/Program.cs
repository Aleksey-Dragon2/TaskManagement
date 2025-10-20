using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Implementation;
using TaskManagement.Infrastructure.Abstractions;
using TaskManagement.Infrastructure.Factories;
using TaskManagement.Infrastructure.Migrations;
using TaskManagement.Infrastructure.Repositories;
namespace TaskManagement;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        UpdateDatabase(scope.ServiceProvider);
        var ui = scope.ServiceProvider.GetRequiredService<ConsoleUi>();
        await ui.RunAsync();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(config.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(CreateTasksTable).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole());

        services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>(sp =>
            new SqlConnectionFactory(config.GetConnectionString("DefaultConnection")));
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<ConsoleUi>();
    }

    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}