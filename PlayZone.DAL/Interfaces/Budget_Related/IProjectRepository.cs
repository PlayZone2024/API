﻿using PlayZone.DAL.Entities.Budget_Related;

namespace PlayZone.DAL.Interfaces.Budget_Related;

public interface IProjectRepository
{
    public IEnumerable<Project> GetAll();

    public IEnumerable<Project> GetByOrgaId(int id);
    public Project? GetById(int id);
    public int Create(Project project);
    public bool Update(Project project);

    public bool Delete(int id);
}