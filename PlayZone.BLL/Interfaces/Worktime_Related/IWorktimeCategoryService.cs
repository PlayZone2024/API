﻿using PlayZone.BLL.Models.Worktime_Related;

namespace PlayZone.BLL.Interfaces.Worktime_Related;

public interface IWorktimeCategoryService
{
    public IEnumerable<WorktimeCategory> GetAll();
    public WorktimeCategory? GetById(int id);
    public int Create(WorktimeCategory worktimeCategory);
    public bool Update(WorktimeCategory worktimeCategory);
    public bool Delete(int id);
}
