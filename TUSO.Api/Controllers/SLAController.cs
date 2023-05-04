using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Emon
 * Date created: 20.09.2022
 * Last modified: 20.09.2022 
 * Modified by: Emon
 */

namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class SLAController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public SLAController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/sla
        /// </summary>
        /// <param name="sla">object to be saved in the table as a row</param>
        /// <returns>Saved object</returns>
        [HttpPost]
        [Route(RouteConstants.CreateSLA)]
        public async Task<IActionResult> CreateSLA(SLA sla)
        {
            try
            {
                context.SLARepository.Add(sla);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadSLAByKey", new { id = sla.OID }, sla);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/slas
        /// </summary>
        /// <returns>List of sla</returns>
        [HttpGet]
        [Route(RouteConstants.ReadSLAs)]
        public async Task<IActionResult> ReadSLAs()
        {
            try
            {
                var slaInDb = await context.SLARepository.ReadSLAs();

                return Ok(slaInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/sla/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Instance of the table object</returns>
        [HttpGet]
        [Route(RouteConstants.ReadSLAByKey)]
        public async Task<IActionResult> ReadSLAByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var slaInDb = await context.SLARepository.GetSlaByKey(key);

                if (slaInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(slaInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/sla/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sla"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(RouteConstants.UpdateSLA)]
        public async Task<IActionResult> UpdateSLA(int key, SLA sla)
        {
            try
            {
                if (key != sla.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.SLARepository.Update(sla);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/sla/{key}
        /// </summary>
        /// <param name="key">Pirmary key of the table</param>
        /// <returns>Delete a row from the table</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteSLA)]
        public async Task<IActionResult> DeleteSLA(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var slaInDb = await context.SLARepository.GetSlaByKey(key);

                if (slaInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                slaInDb.IsDeleted = true;

                context.SLARepository.Update(slaInDb);
                await context.SaveChangesAsync();

                return Ok(slaInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}