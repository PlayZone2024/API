using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayZone.API.Attributes;
using PlayZone.API.DTOs.User_Related;
using PlayZone.API.Mappers.User_Related;
using PlayZone.BLL.Interfaces.User_Related;
using PlayZone.DAL.Entities.User_Related;

namespace PlayZone.API.Controllers.User_Related
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            this._userRoleService = userRoleService;
        }


        [HttpGet]
        [PermissionAuthorize(Permission.MODIFIER_UTILISATEUR)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserRoleDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<UserRoleDTO> userRoleDtos = this._userRoleService.GetAll().Select(ur => ur.ToDTO());
                return this.Ok(userRoleDtos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{idUser:int}")]
        [PermissionAuthorize(Permission.MODIFIER_UTILISATEUR)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<int>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByUser(int idUser)
        {
            try
            {
                IEnumerable<int> userRoleDtos = this._userRoleService.GetByUser(idUser);
                return this.Ok(userRoleDtos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [PermissionAuthorize(Permission.MODIFIER_UTILISATEUR)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(UserRoleDTO userRoleDTO)
        {
            try
            {
                UserRoleDTO ur = this._userRoleService.Create(userRoleDTO.ToModel()).ToDTO();
                if (userRoleDTO.RoleId == ur.RoleId && userRoleDTO.UserId == ur.UserId)
                {
                    return this.Ok();
                }
            }
            catch (Exception) { /* ignored */}
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        [PermissionAuthorize(Permission.MODIFIER_UTILISATEUR)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(UserRoleDTO userRoleDTO)
        {
            try
            {
                if (this._userRoleService.Delete(userRoleDTO.RoleId, userRoleDTO.UserId))
                {
                    return this.Ok();
                }
            }
            catch (Exception) { /* ignored */}
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}
