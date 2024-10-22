using Microsoft.Data.SqlClient;
using System.Data;

namespace DbClient.Interfaces
{
    public interface ISqlDbClient
    {
        IEnumerable<T> Get<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
        Task<IEnumerable<T>> GetAsync<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
    }
}
