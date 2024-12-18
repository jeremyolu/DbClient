﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace DbClient.Interfaces
{
    public interface ISqlDbClient
    {
        T? GetSingle<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
        Task<T?> GetSingleAsync<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
        IEnumerable<T> Get<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
        Task<IEnumerable<T>> GetAsync<T>(string sql, Func<SqlDataReader, T> data, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
        bool Execute(string sql, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
        Task<bool> ExecuteAsync(string sql, CommandType type = CommandType.Text, Dictionary<string, object?>? inputs = null);
    }
}
