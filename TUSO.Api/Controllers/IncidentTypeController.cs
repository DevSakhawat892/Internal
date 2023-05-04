using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 04.09.2022
 * Last modified: 10.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class IncidentTypeController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public IncidentTypeController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/incident-type
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateIncidentType)]
        public async Task<IActionResult> CreateIncidentType(IncidentType incidentType)
        {
            try
            {
                if (await IsIncidentTypeDuplicate(incidentType) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.NoMatchFoundError);

                context.IncidentTypeRepository.Add(incidentType);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadIncidentTypeByKey", new { key = incidentType.OID }, incidentType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/incident-types
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIncidentTypes)]
        public async Task<IActionResult> ReadIncidentTypes()
        {
            try
            {
                var incidentTypeInDb = await context.IncidentTypeRepository.GetIncidentTypes();
                return Ok(incidentTypeInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/incident-type/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Countries</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIncidentTypeByKey)]
        public async Task<IActionResult> ReadIncidentTypeByKey(int OID)
        {
            try
            {
                if (OID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var incidentTypeInDb = await context.IncidentTypeRepository.GetIncidentTypeByKey(OID);

                if (incidentTypeInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(incidentTypeInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/incident-type/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="incidentType">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateIncidentType)]
        public async Task<IActionResult> UpdateIncidentType(int key, IncidentType incidentType)
        {
            try
            {
                if (key != incidentType.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsIncidentTypeDuplicate(incidentType) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.IncidentTypeRepository.Update(incidentType);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/incident-type/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteIncidentType)]
        public async Task<IActionResult> DeleteIncidentType(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var incidentTypeInDb = await context.IncidentTypeRepository.GetIncidentTypeByKey(key);

                if (incidentTypeInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                incidentTypeInDb.IsDeleted = true;

                context.IncidentTypeRepository.Update(incidentTypeInDb);
                await context.SaveChangesAsync();

                return Ok(incidentTypeInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

      /// <summary>
      /// Checks whether the incidentType is duplicate?
      /// </summary>
      /// <param name="incidentType">incidentType object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsIncidentTypeDuplicate(IncidentType incidentType)
        {
            try
            {
                var incidentTypeInDb = await context.IncidentTypeRepository.GetIncidentTypeByName(incidentType.IncidentTypeName);

                if (incidentTypeInDb != null)
                    if (incidentTypeInDb.OID != incidentType.OID)
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