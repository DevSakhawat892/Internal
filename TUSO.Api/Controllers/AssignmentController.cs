using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 05.09.2022
 * Last modified: 10.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IUnitOfWork context;

        public AssignmentController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/assignment
        /// </summary>
        /// <param name="assignment">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateAssignment)]
        public async Task<IActionResult> CreateAssignment(Assignment assignment)
        {
            try
            {
                context.AssignmentRepository.Add(assignment);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadAssignmentByKey", new { key = assignment.OID }, assignment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/assignments
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadAssignments)]
        public async Task<IActionResult> ReadAssignments()
        {
            try
            {
                var country = await context.AssignmentRepository.GetAssignments();
                return Ok(country);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/assignment/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Assignments</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadAssignmentByKey)]
        public async Task<IActionResult> ReadAssignmentByKey(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var assignment = await context.AssignmentRepository.GetAssignmentByKey(key);

                if (assignment == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(assignment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/assignment/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="assignment">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateAssignment)]
        public async Task<IActionResult> UpdateAssignment(long key, Assignment assignment)
        {
            try
            {
                if (key != assignment.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.AssignmentRepository.Update(assignment);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/assignment/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteAssignment)]
        public async Task<IActionResult> DeleteAssignment(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var assignmentInDb = await context.AssignmentRepository.GetAssignmentByKey(key);

                if (assignmentInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                assignmentInDb.IsDeleted = true;

                context.AssignmentRepository.Update(assignmentInDb);
                await context.SaveChangesAsync();

                return Ok(assignmentInDb);
            }
            catch (Exception ex)
            {
                ///WriteToLog(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}