using FluentMigrator;
namespace TaskManagement.Infrastructure.Migrations;

[Migration(2025101601)]
public class CreateTasksTable : Migration 
{
    public override void Up()
    {
        Create.Table("Tasks")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("IsCompleted").AsBoolean().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Tasks");
    }
}