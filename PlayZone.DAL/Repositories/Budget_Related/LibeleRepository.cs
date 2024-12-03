﻿using Dapper;
using Npgsql;
using PlayZone.DAL.Entities.Budget_Related;
using PlayZone.DAL.Interfaces.Budget_Related;

namespace PlayZone.DAL.Repositories.Budget_Related;

public class LibeleRepository : ILibeleRepository
{
    private readonly NpgsqlConnection _connection;

    public LibeleRepository(NpgsqlConnection connection)
    {
        this._connection = connection;
    }

    public IEnumerable<Libele> GetAll()
    {
        const string query = @"
                SELECT
                    ""id_libele"" AS ""IdLibele"",
                    ""category_id"" AS ""IdCategory"",
                    ""name"" AS ""Name""
                FROM ""Libele"";
            ";
        return this._connection.Query<Libele>(query);
    }

    public Libele? GetById(int id)
    {
        const string query = @"
                SELECT
                    ""id_libele"" AS ""IdLibele"",
                    ""category_id"" AS ""IdCategory"",
                    ""name"" AS ""Name""
                FROM ""Libele""
                WHERE ""id_libele"" = @IdLibele;
            ";
        return this._connection.QuerySingleOrDefault<Libele>(query, new { IdLibele = id });
    }

    public int Create(Libele libele)
    {
        const string query = @"
                INSERT INTO ""Libele"" (
                    ""category_id"",
                    ""name""
                )
                VALUES (
                    @IdCategory,
                    @Name
                )
                RETURNING ""id_libele"" AS ""IdLibele"";
            ";
        int resultId = this._connection.QuerySingle<int>(query, new
        {
            IdCategory = libele.IdCategory,
            Name = libele.Name
        });

        return resultId;
    }

    public bool Update(Libele libele)
    {
        const string query = @"
                UPDATE ""Libele"" SET
                    ""category_id"" = @IdCategory,
                    ""name"" = @Name
                WHERE ""id_libele"" = @IdLibele;
            ";
        int affectedRows = this._connection.Execute(query, new
        {
            IdCategory = libele.IdCategory,
            Name = libele.Name,
            IdLibele = libele.IdLibele
        });
        return affectedRows > 0;
    }

    public bool Delete(int id)
    {
        const string query = @"DELETE FROM ""Libele"" WHERE ""id_libele"" = @IdLibele;";
        int affectedRows = this._connection.Execute(query, new { IdLibele = id });
        return affectedRows > 0;
    }
}