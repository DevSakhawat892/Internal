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
    public class IncidentController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default contructor
        /// </summary>
        /// <param name="context"></param>
        public IncidentController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URl: tuso-api/incident
        /// </summary>
        /// <param name="incident">Object to be saved in the table as a row</param>
        /// <returns>Saved object</returns>
        [HttpPost]
        [Route(RouteConstants.CreateIncident)]
        public async Task<IActionResult> CreateIncident(Incident incident)
        {
            try
            {
                context.IncidentRepository.Add(incident);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadIncidentByKey", new { key = incident.OID }, incident);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/incidents
        /// </summary>
        /// <returns>List of table object</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIncidents)]
        public async Task<IActionResult> ReadIncidents()
        {
            try
            {
                var incidentInDb = await context.IncidentRepository.GetIncidents();
                return Ok(incidentInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/incident/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Instance of the table object</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIncidentByKey)]
        public async Task<IActionResult> ReadIncidentByKey(long key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var incidentInDb = await context.IncidentRepository.GetIncidentByKey(key);

                if (incidentInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(incidentInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/incident/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="incident">Object to be updated</param>
        /// <returns>Update row in the table</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateIncident)]
        public async Task<IActionResult> UpdateIncident(long key, Incident incident)
        {
            try
            {
                if (key != incident.OID)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.IncidentRepository.Update(incident);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/incident/{key}
        /// </summary>
        /// <param name="key>Primary key of the table</param>
        /// <returns>Object to be deleted.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteIncident)]
        public async Task<IActionResult> DeleteIncident(long key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var incidentInDb = await context.IncidentRepository.GetIncidentByKey(key);

                if (incidentInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                context.IncidentRepository.Update(incidentInDb);
                await context.SaveChangesAsync();

                return Ok(incidentInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}