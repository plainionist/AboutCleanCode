using System;
using System.Collections.Generic;
using Athena.Adapters.DataAccess;
using Microsoft.Data.Sqlite;

namespace Athena.IO.DataAccess;

internal class SqlRepository : IDatabase
{
    private readonly SqliteConnection myConnection;

    public SqlRepository(string connectionString)
    {
        myConnection = new SqliteConnection(connectionString);
        myConnection.Open();
    }

    public IEnumerable<ImprovementDTO> GetBacklog()
    {
        var cmd = myConnection.CreateCommand();
        cmd.CommandText = "SELECT * FROM Improvements ....";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            yield return new ImprovementDTO
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString(),
                IterationPath = reader["IterationPath"].ToString(),
                AssignedTo = reader["AssignedTo"].ToString(),
                // TODO: add WorkPackages
            };
        }
    }

    public void CreateSchema()
    {
        var cmd = myConnection.CreateCommand();
        cmd.CommandText = "CREATE TABLE Improvements { ... }";
        cmd.ExecuteNonQuery();

        // TODO: create other tables
    }
}
