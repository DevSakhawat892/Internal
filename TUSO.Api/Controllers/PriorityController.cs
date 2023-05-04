using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 17.09.2022
 * Last modified: 17.09.2022, 20.09.2022
 * Modified by: Sakhawat, Bithy
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IUnitOfWork context;
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public PriorityController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/priority
        /// </summary>
        /// <param name="Priority">Object to be saved in the table as a row.</param>
        /// <returns>Saved Object</returns>
        [HttpPost]
        [Route(RouteConstants.CreatePriority)]
        public async Task<IActionResult> CreatePriority(Priority Priority)
        {
            try
            {
                if (await IsPriorityDuplicate(Priority) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.PriorityRepository.Add(Priority);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadPriorityByKey", new { id = Priority.OID }, Priority);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/priorities
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadPriorities)]
        public async Task<IActionResult> ReadPriorities()
        {
            try
            {
                var priorityInDb = await context.PriorityRepository.GetPriorities();
                return Ok(priorityInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/priority/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table.</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadPriorityByKey)]
        public async Task<IActionResult> ReadPriorityByKey(int key)
        {
            try
            {
                var priorityInDb = await context.PriorityRepository.GetPriorityByKey(key);

                if (priorityInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(priorityInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/priority/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(RouteConstants.UpdatePriority)]
        public async Task<IActionResult> UpdatePriority(int key, Priority priority)
        {
            try
            {
                if (key < 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsPriorityDuplicate(priority) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                var priorityInDb = await context.PriorityRepository.GetPriorityByKey(key);

                context.PriorityRepository.Update(priority);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/priority/{key}
        /// </summary>
        /// <param name="key">Primary key of the table.</param>
        /// <returns>Delete a row form the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeletePriority)]
        public async Task<IActionResult> DeletePriority(int key)
        {
            try
            {
                if (key < 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var priorityInDb = await context.PriorityRepository.GetPriorityByKey(key);

                if (priorityInDb == null)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.GenericError);

                priorityInDb.IsDeleted = true;

                context.PriorityRepository.Update(priorityInDb);
                await context.SaveChangesAsync();

                return Ok(priorityInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the Priority name is duplicate.
        /// </summary>
        /// <param name="priority">Ojbect of Priority</param>
        /// <returns></returns>
        private async Task<bool> IsPriorityDuplicate(Priority priority)
        {
            try
            {
                var priorityInDb = await context.PriorityRepository.GetPriorityByName(priority.PriorityName);
                if (priorityInDb != null)
                    if (priorityInDb.OID != priority.OID)
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