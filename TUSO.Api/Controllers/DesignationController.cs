using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public DesignationController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/designation
        /// </summary>
        /// <param name="designation">object to be saved in the table as a row</param>
        /// <returns>Saved object</returns>
        [HttpPost]
        [Route(RouteConstants.CreateDesignation)]
        public async Task<IActionResult> CreateDesignation(Designation designation)
        {
            try
            {
                if (await IsDesignationDuplicate(designation) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.DesignationRepository.Add(designation);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadDesignationByKey", new { id = designation.OID }, designation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/designations
        /// </summary>
        /// <returns>List of designation</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDesignations)]
        public async Task<IActionResult> ReadDesignations()
        {
            try
            {
                var designationInDb = await context.DesignationRepository.GetDesignations();

                return Ok(designationInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/designation/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Instance of the table object</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDesignationByKey)]
        public async Task<IActionResult> ReadDesignationByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var designationInDb = await context.DesignationRepository.GetDesignationBykey(key);

                if (designationInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(designationInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/designation/department?DepartmentID={DepartmentID}
        /// </summary>
        /// <param name="RoleID">DepartmentID of department as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDesignationByDepartment)]
        public async Task<IActionResult> ReadDesignationByDepartment(int DepartmentID)
        {
            try
            {
                if (DepartmentID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.DesignationRepository.GetDesignationByDepartment(DepartmentID);

                if (permissionInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(permissionInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/designation/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="designation"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(RouteConstants.UpdateDesignation)]
        public async Task<IActionResult> UpdateDesignation(int key, Designation designation)
        {
            try
            {
                if (key != designation.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsDesignationDuplicate(designation) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.DesignationRepository.Update(designation);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/designation/{key}
        /// </summary>
        /// <param name="key">Pirmary key of the table</param>
        /// <returns>Delete a row from the table</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteDesignation)]
        public async Task<IActionResult> DeleteDesignation(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var designationInDb = await context.DesignationRepository.GetDesignationBykey(key);

                if (designationInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                designationInDb.IsDeleted = true;

                context.DesignationRepository.Update(designationInDb);
                await context.SaveChangesAsync();

                return Ok(designationInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Check whether the designation is duplicate.
        /// </summary>
        /// <param name="designation">Designation object</param>
        /// <returns>boolean</returns>
        private async Task<bool> IsDesignationDuplicate(Designation designation)
        {
            try
            {
                var designationInDb = await context.DesignationRepository.GetDesignationByName(designation.DesignationName);

                if (designationInDb != null)
                    if (designationInDb.OID != designation.OID)
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