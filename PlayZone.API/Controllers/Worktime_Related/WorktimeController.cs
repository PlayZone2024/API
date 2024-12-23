using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayZone.API.Attributes;
using PlayZone.API.DTOs.Worktime_Related;
using PlayZone.API.Mappers.Worktime_Related;
using PlayZone.BLL.Exceptions;
using PlayZone.BLL.Interfaces.Worktime_Related;
using PlayZone.DAL.Entities.User_Related;
using Models = PlayZone.BLL.Models.Worktime_Related;

namespace PlayZone.API.Controllers.Worktime_Related;

[Route("api/[controller]")]
[ApiController]
public class WorktimeController : ControllerBase
{
    private readonly IWorktimeService _worktimeService;

    public WorktimeController(IWorktimeService worktimeService)
    {
        this._worktimeService = worktimeService;
    }

    public static bool CheckHasPermissionWorktime(ClaimsPrincipal user, int id)
    {
        if (user.HasClaim("Permission", Permission.PERSO_CONSULTER_POINTAGE) &&
            !user.HasClaim("Permission", Permission.ALL_CONSULTER_POINTAGES) &&
            user.HasClaim(ClaimTypes.NameIdentifier, id.ToString()) == false)
        {
            return false;
        }
        return true;
    }

    [HttpGet("range")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorktimeDTO>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByDateRange([FromQuery] int userId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (!CheckHasPermissionWorktime(this.User, userId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        try
        {
            IEnumerable<WorktimeDTO> worktimes =
                this._worktimeService.GetByDateRange(userId, startDate, endDate).Select(w => w.ToDTO());
            return this.Ok(worktimes);
        }
        catch (Exception)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("day/{userId:int}/{dayOfMonth:int}/{monthOfYear:int}/{year:int}")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorktimeDTO>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByDay(int userId, int dayOfMonth, int monthOfYear, int year)
    {
        if (!CheckHasPermissionWorktime(this.User, userId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        try
        {
            IEnumerable<WorktimeDTO> worktimes = this._worktimeService.GetByDay(userId, dayOfMonth, monthOfYear, year)
                .Select(w => w.ToDTO());
            return this.Ok(worktimes);
        }
        catch (Exception)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("week/{userId:int}/{weekOfYear:int}/{year:int}")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorktimeDTO>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByWeek(int userId, int weekOfYear, int year)
    {
        if (!CheckHasPermissionWorktime(this.User, userId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        try
        {
            IEnumerable<WorktimeDTO> worktimes =
                this._worktimeService.GetByWeek(userId, weekOfYear, year).Select(w => w.ToDTO());
            return this.Ok(worktimes);
        }
        catch (Exception)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("month/{userId:int}/{monthOfYear:int}/{year:int}")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorktimeDTO>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByMonth(int userId, int monthOfYear, int year)
    {
        if (!CheckHasPermissionWorktime(this.User, userId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        try
        {
            IEnumerable<WorktimeDTO> worktimes =
                this._worktimeService.GetByMonth(userId, monthOfYear, year).Select(w => w.ToDTO());
            return this.Ok(worktimes);
        }
        catch (Exception)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id:int}")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorktimeDTO))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetById(int id)
    {
        try
        {
            WorktimeDTO? worktime = this._worktimeService.GetById(id)?.ToDTO();
            if (worktime != null)
            {
                if (!CheckHasPermissionWorktime(this.User, worktime.UserId))
                    return this.StatusCode(StatusCodes.Status403Forbidden);
                return this.Ok(worktime);
            }

            return this.NotFound("La plage horaire est introuvable.");
        }
        catch (Exception)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorktimeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Create([FromBody] WorktimeUpdateFormDTO worktime)
    {
        if (!CheckHasPermissionWorktime(this.User, worktime.UserId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        try
        {
            int resultId = this._worktimeService.Create(worktime.ToModel());
            if (resultId > 0)
            {
                WorktimeDTO wt = this._worktimeService.GetById(resultId)!.ToDTO();
                return this.CreatedAtAction(nameof(this.GetById), new { id = resultId }, wt);
            }
        }
        catch (WorktimeAlreadyExistException e)
        {
            return this.BadRequest(e.Message);
        }
        catch (Exception)
        {
            /* ignored */
        }

        return this.StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPut("{id}")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] WorktimeUpdateFormDTO worktime)
    {
        if (!CheckHasPermissionWorktime(this.User, worktime.UserId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        if (id <= 0)
        {
            return this.BadRequest("Invalid user data");
        }

        Models.Worktime updatedWorktime = worktime.ToModel();
        updatedWorktime.IdWorktime = id;
        if (this._worktimeService.Update(updatedWorktime))
        {
            return this.Ok();
        }

        return this.StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("{idWorktime:int}")]
    [Authorize]
    [PermissionAuthorize([Permission.PERSO_CONSULTER_POINTAGE, Permission.ALL_CONSULTER_POINTAGES])]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int idWorktime)
    {
        WorktimeDTO? worktime = this._worktimeService.GetById(idWorktime)?.ToDTO();

        if (worktime == null)
            return this.StatusCode(StatusCodes.Status404NotFound);

        if (!CheckHasPermissionWorktime(this.User, worktime.UserId))
            return this.StatusCode(StatusCodes.Status403Forbidden);

        try
        {
            return this.Ok(this._worktimeService.Delete(idWorktime));
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
}
