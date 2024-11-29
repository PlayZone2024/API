﻿using PlayZone.BLL.Models.Budget_Related;

namespace PlayZone.BLL.Interfaces.Budget_Related;


public interface IProjectService
{
    public IEnumerable<Project> GetALL();
    public IEnumerable<Project> GetByOrgaId(int id);
    public Project? GetById(int id);

    public int Create(Project project);
    public bool Update(Project project);

}
