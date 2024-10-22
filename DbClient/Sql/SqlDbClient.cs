using DbClient.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DbClient.Sql
{
    public class SqlDbClient : ISqlDbClient
    {
        private static string? _connectionString;

        public SqlDbClient(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Connection string cannot be null or empty", nameof(connectionString));

            _connectionString = connectionString;
        }

        public IEnumerable<T> Get<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null)
        {
            var rows = new List<T>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;

            if (inputs != null)
            {
                foreach (var input in inputs)
                {
                    cmd.Parameters.AddWithValue($"@{input.Key}", input.Value ?? DBNull.Value);
                }
            }

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rows.Add(data(reader));
            }

            return rows;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null)
        {
            var rows = new List<T>();

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;

            if (inputs != null)
            {
                foreach (var input in inputs)
                {
                    cmd.Parameters.AddWithValue($"@{input.Key}", input.Value ?? DBNull.Value);
                }
            }

            using var reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                rows.Add(data(reader));
            }

            return rows;
        }
    }
}
