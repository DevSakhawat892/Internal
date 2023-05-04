using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Emon
 * Date created: 24.09.2022
 * Last modified: 24.09.2022, 17.09.2022 
 * Modified by: Emon
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class TicketRoutingController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public TicketRoutingController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/ticket-routing
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateTicketRouting)]
        public async Task<IActionResult> CreateTicketRouting(TicketRouting ticketRouting)
        {
            try
            {
                if (await IsTicketRoutingDuplicate(ticketRouting) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.NoMatchFoundError);

                context.TicketRoutingRepository.Add(ticketRouting);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadTicketRoutingByKey", new { key = ticketRouting.OID }, ticketRouting);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/ticket-routings
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadTicketRoutings)]
        public async Task<IActionResult> ReadTicketRoutings()
        {
            try
            {
                var ticketRoutingInDb = await context.TicketRoutingRepository.GetTicketRoutings();
                return Ok(ticketRoutingInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/ticket-routing/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table TicketRouting</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadTicketRoutingByKey)]
        public async Task<IActionResult> ReadTicketRoutingByKey(int OID)
        {
            try
            {
                if (OID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var ticketRoutingInDb = await context.TicketRoutingRepository.GetTicketRoutingByKey(OID);

                if (ticketRoutingInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(ticketRoutingInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/ticket-routing/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="ticketRouting">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateTicketRouting)]
        public async Task<IActionResult> UpdateTicketRouting(int key, TicketRouting ticketRouting)
        {
            try
            {
                if (key != ticketRouting.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsTicketRoutingDuplicate(ticketRouting) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.TicketRoutingRepository.Update(ticketRouting);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/ticket-routing/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteTicketRouting)]
        public async Task<IActionResult> DeleteTicketRouting(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var ticketRoutingInDb = await context.TicketRoutingRepository.GetTicketRoutingByKey(key);

                if (ticketRoutingInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                ticketRoutingInDb.IsDeleted = true;

                context.TicketRoutingRepository.Update(ticketRoutingInDb);
                await context.SaveChangesAsync();

                return Ok(ticketRoutingInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the ticketRouting is duplicate?
        /// </summary>
        /// <param name="routeType">TicketRouting object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsTicketRoutingDuplicate(TicketRouting ticketRouting)
        {
            try
            {
                var ticketRoutingInDb = await context.TicketRoutingRepository.GetTicketRoutingByKey(ticketRouting.RouteTypeID);

                if (ticketRoutingInDb != null)
                    if (ticketRoutingInDb.OID != ticketRouting.OID)
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