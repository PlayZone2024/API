﻿using Dapper;
using Npgsql;
using PlayZone.DAL.Entities.Budget_Related;
using PlayZone.DAL.Interfaces.Budget_Related;

namespace PlayZone.DAL.Repositories.Budget_Related;

public class PrevisionBudgetLibeleRepository : IPrevisionBudgetLibeleRepository
{
    private readonly NpgsqlConnection _connection;

    public PrevisionBudgetLibeleRepository(NpgsqlConnection connection)
    {
        this._connection = connection;
    }

    public IEnumerable<PrevisionBudgetLibele> GetByIdProject(int IdProject)
    {
        const string query = @"
            SELECT
                ""id_prevision_budget_libele"" AS ""IdPrevisionBudgetLibele"" ,
                ""project_id"" AS ""IdProject"",
                ""libele_id"" AS ""IdLibele"",
                ""date"" AS ""Date"",
                ""motif"" AS ""Motif"",
                ""montant"" AS ""Montant""
            FROM ""Prevision_Budget_Libele""
            WHERE ""project_id"" = @IdProject;
        ";
        return this._connection.Query<PrevisionBudgetLibele>(query, new { IdProject = IdProject });
    }

    public PrevisionBudgetLibele? GetById(int id)
    {
        const string query = @"
            SELECT
                ""id_prevision_budget_libele"" AS ""IdPrevisionBudgetLibele"" ,
                ""project_id"" AS ""IdProject"",
                ""libele_id"" AS ""IdLibele"",
                ""date"" AS ""Date"",
                ""motif"" AS ""Motif"",
                ""montant"" AS ""Montant""
            FROM ""Prevision_Budget_Libele""
            WHERE ""id_prevision_budget_libele"" = @id;
        ";
        return this._connection.QuerySingleOrDefault<PrevisionBudgetLibele>(query, new { id = id });
    }

    public int Create(PrevisionBudgetLibele previsionBudgetLibele)
    {
        const string query = @"
                INSERT INTO ""Prevision_Budget_Libele"" (
                    ""project_id"",
                    ""libele_id"",
                    ""date"",
                    ""motif"",
                    ""montant""
                    )
                VALUES (
                    @Name
                    )
                RETURNING ""id_prevision_budget_libele"";
        ";
        int resultId = this._connection.QuerySingle<int>(query, new
        {
            IdProject = previsionBudgetLibele.IdProject,
            IdLibele = previsionBudgetLibele.IdLibele,
            Date = previsionBudgetLibele.Date,
            Motif = previsionBudgetLibele.Motif,
            Montant = previsionBudgetLibele.Montant,

        });

        return resultId;
    }

    public bool Update(PrevisionBudgetLibele previsionBudgetLibele)
    {
        const string query = @"
            UPDATE ""Prevision_Budget_Libele"" SET
                ""date"" = @Date,
                ""motif"" = @Motif,
                ""montant"" = @Montant
            WHERE ""id_prevision_budget_libele"" = @IdPrevisionBudgetLibele
        ";
        int affectedRows = this._connection.Execute(query, new
        {
            Date = previsionBudgetLibele.Date,
            Motif = previsionBudgetLibele.Motif,
            Montant = previsionBudgetLibele.Montant
        });
        return affectedRows > 0;
    }

    public bool Delete(int id)
    {
        const string query = @"DELETE FROM ""Prevision_Budget_Libele"" WHERE ""id_prevision_budget_libele"" = @IdPrevisionBudgetLibele;";
        int affectedRows = this._connection.Execute(query, new { IdPrevisionBudgetLibele = id });
        return affectedRows > 0;
    }
}