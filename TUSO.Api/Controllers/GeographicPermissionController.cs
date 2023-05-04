using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 17.09.2022, 20.09.2022 
 * Modified by: Rakib, Bithy
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class GeographicPermissionController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Geographic Permission constructor.
        /// </summary>
        /// <param name="context">Inject IUnitOfWork as context</param>
        public GeographicPermissionController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/geographic-permission
        /// </summary>
        [HttpPost]
        [Route(RouteConstants.CreateGeographicPermission)]
        public async Task<IActionResult> CreateGeographicPermission(GeographicPermission permission)
        {
            try
            {
                context.GeographicPermissionRepository.Add(permission);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadGeographicPermissionByKey", new { key = permission.OID }, permission);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/geographic-permissions
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadGeographicPermissions)]
        public async Task<IActionResult> ReadGeographicPermissions()
        {
            try
            {
                var permissionInDb = await context.GeographicPermissionRepository.GetGeographicPermissions();
                return Ok(permissionInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/geographic-permission/key/{key}
        /// </summary>
        /// <param name="OID">Primary key of entity as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadGeographicPermissionByKey)]
        public async Task<IActionResult> ReadGeographicPermissionByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.GeographicPermissionRepository.GetGeographicPermissionByKey(key);

                if (permissionInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(permissionInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/geographic-permission/useraccount?UserAccountID={UserAccountID}
        /// </summary>
        /// <param name="UserAccountID">UserAccountID of useraccount as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadGeographicPermissionByUser)]
        public async Task<IActionResult> ReadGeographicPermissionByUser(int UserAccountID)
        {
            try
            {
                if (UserAccountID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.GeographicPermissionRepository.GetGeographicPermissionByUser(UserAccountID);

                if (permissionInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(permissionInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/geographic-permission/province?ProvinceID={ProvinceID}
        /// </summary>
        /// <param name="ProvinceID">ProvinceID of province as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadGeographicPermissionByProvince)]
        public async Task<IActionResult> ReadGeographicPermissionByProvince(int ProvinceID)
        {
            try
            {
                if (ProvinceID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.GeographicPermissionRepository.GetGeographicPermissionByProvince(ProvinceID);

                if (permissionInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(permissionInDb);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/geographic-permission/{key}
        /// </summary>
        /// <param name="key">OID of the entity as parameter</param>
        /// <param name="permission">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateGeographicPermission)]
        public async Task<IActionResult> UpdateGeographicPermission(int key, GeographicPermission permission)
        {
            try
            {
                if (key != permission.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.GeographicPermissionRepository.Update(permission);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/geographic-permission/{key}
        /// </summary>
        /// <param name="key">OID of the entity as parameter</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteGeographicPermission)]
        public async Task<IActionResult> DeleteGeographicPermission(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.GeographicPermissionRepository.GetGeographicPermissionByKey(key);

                if (permissionInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                permissionInDb.IsDeleted = true;

                context.GeographicPermissionRepository.Update(permissionInDb);
                await context.SaveChangesAsync();

                return Ok(permissionInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}