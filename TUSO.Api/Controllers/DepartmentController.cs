using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Emon
 * Date created: 13.09.2022
 * Last modified: 13.09.2022
 * Modified by: Emon
 */
namespace TUSO.Api.Controllers
{
    /// <summary>
    ///Department Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="context"></param>
        public DepartmentController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/department
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateDepartment)]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            try
            {
                if (await IsDepartmentDuplicate(department) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.DepartmentRepository.Add(department);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadDepartmentByKey", new { id = department.OID }, department);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/department
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDepartments)]
        public async Task<IActionResult> ReadDepartments()
        {
            try
            {
                var department = await context.DepartmentRepository.GetDepartments();
                return Ok(department);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/department/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Departments</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDepartmentByKey)]
        public async Task<IActionResult> ReadDepartmentByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var department = await context.DepartmentRepository.GetDepartmentByKey(key);

                if (department == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(department);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/department/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="country">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateDepartment)]
        public async Task<IActionResult> UpdateDepartment(int key, Department department)
        {
            try
            {
                if (key != department.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsDepartmentDuplicate(department) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.DepartmentRepository.Update(department);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/department/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteDepartment)]
        public async Task<IActionResult> DeleteDepartment(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var departmentInDb = await context.DepartmentRepository.GetDepartmentByKey(key);

                if (departmentInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                departmentInDb.IsDeleted = true;

                context.DepartmentRepository.Update(departmentInDb);
                await context.SaveChangesAsync();

                return Ok(departmentInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the department name is duplicate? 
        /// </summary>
        /// <param name="department">Department object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsDepartmentDuplicate(Department department)
        {
            try
            {
                var departmentInDb = await context.DepartmentRepository.GetDepartmentByName(department.DepartmentName);

                if (departmentInDb != null)
                    if (departmentInDb.OID != department.OID)
                        return true;

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}