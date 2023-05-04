using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 05.09.2022
 * Last modified: 10.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class IncidentStatusController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default contructor
        /// </summary>
        /// <param name="context"></param>
        public IncidentStatusController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URl: tuso-api/incidentStatus
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpGet]
        [Route(RouteConstants.CreateIncidentStatus)]
        public async Task<IActionResult> CreateIncidentStatus(IncidentStatus incidentStatus)
        {
            try
            {
                context.IncidentStatusRepository.Add(incidentStatus);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadIncidentStatusByKey", new { key = incidentStatus.OID }, incidentStatus);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/incident-statuses
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIncidentStatuses)]
        public async Task<IActionResult> ReadIncidentStatuses()
        {
            try
            {
                var incidentStatusInDb = await context.IncidentStatusRepository.GetIncidentStatuses();
                return Ok(incidentStatusInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/incident-status/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table incident-statuses</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIncidentStatusByKey)]
        public async Task<IActionResult> ReadIncidentStatusByKey(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var incidentStatusInDb = await context.IncidentStatusRepository.GetIncidentStatusByKey(key);

                if (incidentStatusInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(incidentStatusInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/incident-status/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="incidentStatus">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateIncidentStatus)]
        public async Task<IActionResult> incidentStatus(Guid key, IncidentStatus incidentStatus)
        {
            try
            {
                if (string.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.IncidentStatusRepository.Update(incidentStatus);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/incident-status/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteIncidentStatus)]
        public async Task<IActionResult> DeleteIncidentStatus(long key)
        {
            try
            {
                if (string.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var incidentStatusInDb = await context.IncidentStatusRepository.GetIncidentStatusByKey(key);

                if (incidentStatusInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                incidentStatusInDb.IsDeleted = true;

                context.IncidentStatusRepository.Update(incidentStatusInDb);
                await context.SaveChangesAsync();

                return Ok(incidentStatusInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}