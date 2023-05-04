using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib
 * Date created: 04.09.2022
 * Last modified: 10.09.2022,17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class ApplicationPermissionController : ControllerBase
   {
      private readonly IUnitOfWork context;

      /// <summary>
      /// Application Permission constructor.
      /// </summary>
      /// <param name="context">Inject IUnitOfWork as context</param>
      public ApplicationPermissionController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: tuso-api/application-permission
      /// </summary>
      [HttpPost]
      [Route(RouteConstants.CreateApplicationPermission)]
      public async Task<IActionResult> CreateApplicationPermission(ApplicationPermission permission)
      {
         try
         {
            context.ApplicationPermissionRepository.Add(permission);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadApplicationPermissionByKey", new { key = permission.OID }, permission);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/application-permissions
      /// </summary>
      /// <returns>List of table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadApplicationPermissions)]
      public IActionResult ReadApplicationPermissions()
      {
         try
         {
            List<ApplicationPermissionDto> permissionInDb = context.ApplicationPermissionRepository.GetApplicationPermissions();
            return Ok(permissionInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL : tuso-api/application-permission/key/{OID}
      /// </summary>
      /// <param name="OID">Primary key of entity as parameter</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadApplicationPermissionByKey)]
      public async Task<IActionResult> ReadApplicationPermissionByKey(int OID)
      {
         try
         {
            if (OID <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var permissionInDb = await context.ApplicationPermissionRepository.GetApplicationPermissionByKey(OID);

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
      /// URL : tuso-api/project-permission/role?RoleID={RoleID}
      /// </summary>
      /// <param name="RoleID">RoleID of role as parameter</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadApplicationPermissionByRole)]
      public async Task<IActionResult> ReadApplicationPermissionByRole(int RoleID)
      {
         try
         {
            if (RoleID <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var permissionInDb = await context.ApplicationPermissionRepository.GetApplicationPermissionByRole(RoleID);

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
      /// URL : tuso-api/project-permission/module?ModuleID={ModuleID}
      /// </summary>
      /// <param name="ModuleID">ModuleID of module as parameter</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadApplicationPermissionByModule)]
      public async Task<IActionResult> ReadApplicationPermissionByModule(int ModuleID)
      {
         try
         {
            if (ModuleID <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var permissionInDb = await context.ApplicationPermissionRepository.GetApplicationPermissionByModule(ModuleID);

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
      /// URL : tuso-api/application-permission/key?RoleID={RoleID}&ModuleID={ModuleID}
      /// </summary>
      /// <param name="RoleID">RoleID of role as parameter</param>
      /// <param name="ModuleID">ModuleID of module as parameter</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadApplicationPermission)]
      public async Task<IActionResult> ReadApplicationPermission(int RoleID, int ModuleID)
      {
         try
         {
            if (RoleID <= 0 && ModuleID <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var permissionInDb = await context.ApplicationPermissionRepository.GetApplicationPermission(RoleID, ModuleID);

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
      /// URL: tuso-api/application-permission/{key}
      /// </summary>
      /// <param name="key">Primary key of the entity as parameter</param>
      /// <param name="permission">Object to be updated</param>
      /// <returns>Update row in the table.</returns>
      [HttpPut]
      [Route(RouteConstants.UpdateApplicationPermission)]
      public async Task<IActionResult> UpdateApplicationPermission(int key, ApplicationPermission permission)
      {
         try
         {
            if (key != permission.OID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            context.ApplicationPermissionRepository.Update(permission);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/application-permission/{key}
      /// </summary>
      /// <param name="key">Primary key of the entity as parameter</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      [Route(RouteConstants.DeleteApplicationPermission)]
      public async Task<IActionResult> DeleteApplicationPermission(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var permissionInDb = await context.ApplicationPermissionRepository.GetApplicationPermissionByKey(key);

            if (permissionInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            permissionInDb.IsDeleted = true;

            context.ApplicationPermissionRepository.Update(permissionInDb);
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