using System.Data;

namespace TaskManagement.Infrastructure.Abstractions;

public interface IDbConnectionFactory
{
   Task<IDbConnection> CreateDbConnectionAsync(); 
}