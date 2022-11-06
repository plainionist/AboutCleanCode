using System;
using System.Collections.Generic;
using Athena.Adapters.DataAccess;
using Microsoft.Data.Sqlite;

namespace Athena.IO.DataAccess;

internal class SqlRepository : ISqlDatabase
{
    private readonly SqliteConnection myConnection;

    public SqlRepository(string connectionString)
    {
        myConnection = new SqliteConnection(connectionString);
        myConnection.Open();
    }

    public IEnumerable<WorkItemDTO> Query(string sql)
    {
        var cmd = myConnection.CreateCommand();
        cmd.CommandText = sql;

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var fields = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; ++i)
            {
                fields.Add(reader.GetName(i), reader.GetValue(i));
            }

            yield return new WorkItemDTO
            {
                Id = Convert.ToInt32(reader["Id"]),
                Fields = fields
            };
        }
    }

    public void Execute(string sql)
    {
        var cmd = myConnection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
}
