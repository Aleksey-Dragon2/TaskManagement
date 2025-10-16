using System.Data;
using Microsoft.Data.SqlClient;
using TaskManagement.Infrastructure.Abstractions;


namespace TaskManagement.Infrastructure.Factories;

public class SqlConnectionFactory : IDbConnectionFactory
{
   private readonly string _connectionString;
   public SqlConnectionFactory(string connectionString)
   {
      _connectionString = connectionString;
   }

   public async Task<IDbConnection> CreateDbConnectionAsync()
   {
      var conection = new SqlConnection(_connectionString);
      await  conection.OpenAsync();
      return conection; 
   }
}