using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Labib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class RoleController : ControllerBase
   {
      private readonly IUnitOfWork context;

      /// <summary>
      /// Default constructor.
      /// </summary>
      /// <param name="UnitOfWork"></param>
      public RoleController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: tuso-api/user-role
      /// </summary>
      /// <param name="entity">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [HttpPost]
      [Route(RouteConstants.CreateUserRole)]
      public async Task<IActionResult> CreateUserRole(Role role)
      {
         try
         {
            if (await IsRoleDuplicate(role) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            context.RoleRepository.Add(role);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadUserRoleByKey", new { id = role.OID }, role);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/user-roles
      /// </summary>
      /// <returns>List of table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadUserRoles)]
      public async Task<IActionResult> ReadUserAccounts()
      {
         try
         {
            var userAccounts = await context.RoleRepository.GetRoles();
            return Ok(userAccounts);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/user-role/key/{key}
      /// </summary>
      /// <param name="key">Primary key of the table Countries</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadUserRoleByKey)]
      public async Task<IActionResult> ReadUserRoleByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var userAccount = await context.RoleRepository.GetRoleByKey(key);

            if (userAccount == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(userAccount);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/user-role/{key}
      /// </summary>
      /// <param name="key">Primary key of the talbe</param>
      /// <param name="role">Object to be updated</param>
      /// <returns>Update row in the table.</returns>
      [HttpPut]
      [Route(RouteConstants.UpdateUserRole)]
      public async Task<IActionResult> UpdateUserRole(int key, Role role)
      {
         try
         {
            if (key != role.OID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsRoleDuplicate(role) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            var userRole = await context.RoleRepository.GetRoleByKey(key);

            var roleUpdate = new Role
            {
               OID = role.OID,
               RoleName = userRole.RoleName,
               Description = role.Description
            };

            context.RoleRepository.Update(roleUpdate);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/user-role/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      [Route(RouteConstants.DeleteUserRole)]
      public async Task<IActionResult> DeleteUserRole(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var userAccountInDb = context.RoleRepository.Get(key);

            if (userAccountInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            userAccountInDb.IsDeleted = true;

            context.RoleRepository.Update(userAccountInDb);
            await context.SaveChangesAsync();

            return Ok(userAccountInDb);
         }
         catch (Exception)
         {
            ///WriteToLog(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Checks whether the user role is duplicate? 
      /// </summary>
      /// <param name="role">UserRole object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsRoleDuplicate(Role role)
      {
         try
         {
            var userAccountInDb = await context.RoleRepository.GetRoleByName(role.RoleName);

            if (userAccountInDb != null)
               if (userAccountInDb.OID != role.OID)
                  return true;

            return false;
         }
         catch
         {
            throw;
         }
      }
   }

}