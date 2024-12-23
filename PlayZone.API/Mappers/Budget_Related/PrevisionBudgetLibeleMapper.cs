﻿using PlayZone.API.DTOs.Budget_Related;
using Models = PlayZone.BLL.Models.Budget_Related;

namespace PlayZone.API.Mappers.Budget_Related;

public static class PrevisionBudgetLibeleMapper
{
    public static PrevisionBudgetLibeleDTO ToDTO(this Models.PrevisionBudgetLibele previsionBudgetLibele)
    {
        return new PrevisionBudgetLibeleDTO
        {
            IdPrevisionBudgetLibele = previsionBudgetLibele.IdPrevisionBudgetLibele,
            IdProject = previsionBudgetLibele.IdProject,
            CategoryId = previsionBudgetLibele.CategoryId,
            CategoryName = previsionBudgetLibele.CategoryName,
            LibeleId = previsionBudgetLibele.LibeleId,
            LibeleName = previsionBudgetLibele.LibeleName,
            Date = previsionBudgetLibele.Date,
            Motif = previsionBudgetLibele.Motif,
            Montant = previsionBudgetLibele.Montant
        };
    }

    public static Models.PrevisionBudgetLibele ToModel(this PrevisionBudgetLibeleDTO previsionBudgetLibeleDTO)
    {
        return new Models.PrevisionBudgetLibele
        {
            IdPrevisionBudgetLibele = previsionBudgetLibeleDTO.IdPrevisionBudgetLibele,
            IdProject = previsionBudgetLibeleDTO.IdProject,
            LibeleId = previsionBudgetLibeleDTO.LibeleId,
            Date = previsionBudgetLibeleDTO.Date,
            Motif = previsionBudgetLibeleDTO.Motif,
            Montant = previsionBudgetLibeleDTO.Montant
        };
    }

    public static Models.PrevisionBudgetLibele ToModel(this PrevisionBudgetLibeleCreateDTO previsionBudgetLibeleCreateDTO)
    {
        return new Models.PrevisionBudgetLibele
        {
            IdProject = previsionBudgetLibeleCreateDTO.IdProject,
            LibeleId = previsionBudgetLibeleCreateDTO.IdLibele,
            Date = previsionBudgetLibeleCreateDTO.Date,
            Motif = previsionBudgetLibeleCreateDTO.Motif,
            Montant = previsionBudgetLibeleCreateDTO.Montant
        };
    }

    public static Models.PrevisionBudgetLibele ToModel(this PrevisionBudgetLibeleUpdateDTO previsionBudgetLibeleUpdateDTO)
    {
        return new Models.PrevisionBudgetLibele
        {
            IdProject = previsionBudgetLibeleUpdateDTO.IdProject,
            LibeleId = previsionBudgetLibeleUpdateDTO.IdLibele,
            Date = previsionBudgetLibeleUpdateDTO.Date,
            Motif = previsionBudgetLibeleUpdateDTO.Motif,
            Montant = previsionBudgetLibeleUpdateDTO.Montant
        };
    }
}
