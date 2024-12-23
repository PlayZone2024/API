﻿using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using PlayZone.DAL.Entities.Budget_Related;
using PlayZone.DAL.Interfaces.Budget_Related;

namespace PlayZone.DAL.Repositories.Budget_Related;

public class PrevisionRentreeRepository : IPrevisionRentreeRepository
{
    private readonly NpgsqlConnection _connection;
    private ILogger<PrevisionRentreeRepository> _logger;

    public PrevisionRentreeRepository(NpgsqlConnection connection, ILogger<PrevisionRentreeRepository> logger)
    {
        this._connection = connection;
        this._logger = logger;
    }

    public IEnumerable<PrevisionRentree> GetByProject(int projectId)
    {
        const string query = @"
            SELECT
                pr.""id_prevision_rentree"" AS ""IdPrevisionRentree"",
                l.category_id AS ""CategoryId"",
                c.name AS ""CategoryName"",
                pr.""libele_id"" AS ""LibeleId"",
                l.name AS ""LibeleName"",
                pr.""organisme_id"" AS ""OrganismeId"",
                pr.""date"" AS ""Date"",
                pr.""motif"" AS ""Motif"",
                pr.""montant"" AS ""Montant"",
                pr.""project_id"" AS ""ProjectId""
            FROM ""Prevision_Rentree"" pr
            INNER JOIN ""Libele"" l ON pr.libele_id = l.id_libele
            INNER JOIN ""Category"" c ON l.category_id = c.id_category
            WHERE ""project_id"" = @ProjectId;
        ";
        return this._connection.Query<PrevisionRentree>(query, new { ProjectId = projectId });
    }

    public PrevisionRentree? GetById(int id)
    {
        const string query = @"
            SELECT
                pr.""id_prevision_rentree"" AS ""IdPrevisionRentree"",
                l.category_id AS ""CategoryId"",
                c.name AS ""CategoryName"",
                pr.""libele_id"" AS ""LibeleId"",
                l.name AS ""LibeleName"",
                pr.""organisme_id"" AS ""OrganismeId"",
                pr.""date"" AS ""Date"",
                pr.""motif"" AS ""Motif"",
                pr.""montant"" AS ""Montant"",
                pr.""project_id"" AS ""ProjectId""
            FROM ""Prevision_Rentree"" pr
            INNER JOIN ""Libele"" l ON pr.libele_id = l.id_libele
            INNER JOIN ""Category"" c ON l.category_id = c.id_category
            WHERE ""id_prevision_rentree"" = @IdPrevisionRentree;
        ";
        return this._connection.QuerySingleOrDefault<PrevisionRentree>(query, new { IdPrevisionRentree = id });
    }

    public int Create(PrevisionRentree previsionRentree)
    {
        const string query = @"
            INSERT INTO ""Prevision_Rentree"" (
                ""libele_id"",
                ""project_id"",
                ""organisme_id"",
                ""date"",
                ""motif"",
                ""montant""
            )
            VALUES (
                    @LibeleId,
                    @ProjectId,
                    @OrganismeId,
                    @Date,
                    @Motif,
                    @Montant
                    )
            RETURNING ""id_prevision_rentree"" AS ""IdPrevisionRentree"";
                ";
        int resultId = this._connection.QuerySingle<int> (query, new
        {
            IdPrevisionRentree = previsionRentree.IdPrevisionRentree,
            LibeleId = previsionRentree.LibeleId,
            ProjectId = previsionRentree.ProjectId,
            OrganismeId = previsionRentree.OrganismeId,
            Date = previsionRentree.Date,
            Motif = previsionRentree.Motif,
            Montant = previsionRentree.Montant
        });
        return resultId;
    }

    public bool Update(PrevisionRentree previsionRentree)
    {
        const string query = @"
            UPDATE ""Prevision_Rentree""
            SET
                ""libele_id"" = @LibeleId,
                ""project_id"" = @ProjectId,
                ""organisme_id"" = @OrganismeId,
                ""date"" = @Date,
                ""motif"" = @Motif,
                ""montant"" = @Montant
            WHERE ""id_prevision_rentree"" = @IdPrevisionRentree;
                ";
            return this._connection.Execute(query, previsionRentree) > 0;
    }

    public bool Delete(int id)
    {
        const string query = @"DELETE
        FROM
            ""Prevision_Rentree""
        WHERE ""id_prevision_rentree"" = @IdPrevisionRentree;";
        int affectedRows = this._connection.Execute(query, new { IdPrevisionRentree = id });
        return affectedRows > 0;
    }
}
