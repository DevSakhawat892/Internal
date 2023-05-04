using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 17.09.2022
 * Last modified: 17.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class IncidentCategoryController : ControllerBase
   {
      private readonly IUnitOfWork context;

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <param name="context"></param>
      public IncidentCategoryController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: tuso-api/incident-category
      /// </summary>
      /// <param name="incidentCategory">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [Route(RouteConstants.CreateIncidentCategory)]
      [HttpPost]
      public async Task<IActionResult> CreateIncidnetCategory(IncidentCategory incidentCategory)
      {
         try
         {
            if (await IsIncidentCategoryDuplicate(incidentCategory) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);
            context.IncidentCategoryRepository.Add(incidentCategory);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadIncidentCategory", new { id = incidentCategory.OID}, incidentCategory);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/incident-categories
      /// </summary>
      /// <returns>List of table object</returns>
      [Route(RouteConstants.ReadIncidentCategories)]
      [HttpGet]
      public async Task<IActionResult> ReadIncidentCategories()
      {
         try
         {
            var incidentCategories = await context.IncidentCategoryRepository.GetIncidentCategories();
            return Ok(incidentCategories);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/incident-category/key/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <returns>Instance of the table object</returns>
      [Route(RouteConstants.ReadIncidentCategoryByKey)]
      [HttpGet]
      public async Task<IActionResult> ReadIncidentCategoryByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var incidentCategoryInDb = await context.IncidentCategoryRepository.GetIncidentCategoryByKey(key);

            if (incidentCategoryInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(incidentCategoryInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/incident-category/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <param name="incidentCategory">Object to be updated</param>
      /// <returns>Update row in the table</returns>
      [Route(RouteConstants.UpdateIncidentCategory)]
      [HttpPut]
      public async Task<IActionResult> UpdateIncidentCategory(int key, IncidentCategory incidentCategory)
      {
         try
         {
            if (key != incidentCategory.OID)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsIncidentCategoryDuplicate(incidentCategory) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.IncidentCategoryRepository.Update(incidentCategory);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/incident-category/{key}
      /// </summary>
      /// <param name="key">Primary key of the table.</param>
      /// <returns>Delete a row from the table.</returns>
      [Route(RouteConstants.DeleteIncidentCategory)]
      [HttpDelete]
      public async Task<IActionResult> DeleteIncidentCategory(int key)
      {
         try
         {
            if (key < 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var incidentCategoryInDb = await context.IncidentCategoryRepository.GetIncidentCategoryByKey(key);

            if (incidentCategoryInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            incidentCategoryInDb.IsDeleted = true;

            context.IncidentCategoryRepository.Update(incidentCategoryInDb);
            await context.SaveChangesAsync();

            return Ok(incidentCategoryInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Checks whether the country name is duplicate? 
      /// </summary>
      /// <param name="country">Country object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsIncidentCategoryDuplicate(IncidentCategory incidentCategory)
      {
         try
         {
            var incidentCategoryInDb = await context.IncidentCategoryRepository.GetIncidentCategoryByName(incidentCategory.IncidentCategorys);

            if (incidentCategoryInDb != null)
               if (incidentCategoryInDb.OID != incidentCategory.OID)
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