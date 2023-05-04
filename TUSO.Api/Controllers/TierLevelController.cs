using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class TierLevelController : ControllerBase
   {
      private readonly IUnitOfWork context;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public TierLevelController(IUnitOfWork context)
      {
         this.context = context;
      }
      /// <summary>
      /// URL: tuso-api/tier-level
      /// </summary>
      /// <param name="tierLevel">object to be saved in the table as a row</param>
      /// <returns>Saved object</returns>
      [HttpPost]
      [Route(RouteConstants.CreateTierLevel)]
      public async Task<IActionResult> CreateTierLevel(TierLevel tierLevel)
      {
         try
         {
            if (await IsTierLevelDuplicate(tierLevel) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.TierLevelRepository.Add(tierLevel);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadTierLevelByKey", new { id = tierLevel.OID }, tierLevel);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/tier-levels
      /// </summary>
      /// <returns>List of tierLevel</returns>
      [HttpGet]
      [Route(RouteConstants.ReadTierLevels)]
      public async Task<IActionResult> ReadTierLevels()
      {
         try
         {
            var tierLevelInDb = await context.TierLevelRepository.GetTierLevels();

            return Ok(tierLevelInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/tier-level/key/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <returns>Instance of the table object</returns>
      [HttpGet]
      [Route(RouteConstants.ReadTierLevelByKey)]
      public async Task<IActionResult> ReadTierLevelByKey(int OID)
      {
         try
         {
            if (OID <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var tierLevelInDb = await context.TierLevelRepository.GetTierLevelByKey(OID);

            if (tierLevelInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(tierLevelInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/tier-level/{key}
      /// </summary>
      /// <param name="key">Primary key of the talbe</param>
      /// <param name="tierLevel">Object to be updated</param>
      /// <returns>Update row in the table.</returns>
      [HttpPut]
      [Route(RouteConstants.UpdateTierLevel)]
      public async Task<IActionResult> UpdateTierLevel(int key, TierLevel tierLevel)
      {
         try
         {
            if (key != tierLevel.OID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsTierLevelDuplicate(tierLevel) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.TierLevelRepository.Update(tierLevel);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/tier-level/{key}
      /// </summary>
      /// <param name="key">Pirmary key of the table</param>
      /// <returns>Delete a row from the table</returns>
      [HttpDelete]
      [Route(RouteConstants.DeleteTierLevel)]
      public async Task<IActionResult> DeleteTierLevel(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var tierLevelInDb = await context.TierLevelRepository.GetTierLevelByKey(key);

            if (tierLevelInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            tierLevelInDb.IsDeleted = true;

            context.TierLevelRepository.Delete(tierLevelInDb);
            await context.SaveChangesAsync();

            return Ok(tierLevelInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Check whether the tierLevel is duplicate.
      /// </summary>
      /// <param name="tierLevel">TierLevel object</param>
      /// <returns>boolean</returns>
      private async Task<bool> IsTierLevelDuplicate(TierLevel tierLevel)
      {
         try
         {
            var tierLevelInDb = await context.TierLevelRepository.GetTierLevelByName(tierLevel.TierName);

            if (tierLevelInDb != null)
               if (tierLevelInDb.OID != tierLevel.OID)
                  return true;
            return false;
         }
         catch (Exception)
         {

            throw;
         }
      }
   }
}
