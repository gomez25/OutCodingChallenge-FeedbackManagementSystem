using System.Data;
using System.Data.SqlClient;

namespace FeedbackService.Infrastructure.Persistence.Contexts;

public class FeedbackContext(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public string ConnectionString => _connectionString;

    public async Task<bool> ExecuteNonQueryAsync(string storeProcedureName, CommandType commandType = CommandType.StoredProcedure, SqlParameter[] parameters = null)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = new(storeProcedureName, connection);
        command.CommandType = commandType;

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        await connection.OpenAsync();

        int rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    public async Task<SqlDataReader> ExecuteQueryAsync(string storeProcedureName, CommandType commandType = CommandType.StoredProcedure, SqlParameter[] parameters = null)
    {
        SqlConnection connection = new(_connectionString);
        SqlCommand command = new(storeProcedureName, connection)
        {
            CommandType = commandType
        };

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        await connection.OpenAsync();

        return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
    }
}