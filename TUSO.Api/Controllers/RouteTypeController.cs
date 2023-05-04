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
    public class RouteTypeController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public RouteTypeController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/route-type
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateRouteType)]
        public async Task<IActionResult> CreateRouteType(RouteType routeType)
        {
            try
            {
                if (await IsRouteTypeDuplicate(routeType) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.NoMatchFoundError);

                context.RouteTypeRepository.Add(routeType);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadRouteTypeByKey", new { key = routeType.OID }, routeType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/route-types
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadRouteTypes)]
        public async Task<IActionResult> ReadRouteTypes()
        {
            try
            {
                var routeTypeInDb = await context.RouteTypeRepository.GetRouteTypes();
                return Ok(routeTypeInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/route-type/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table RouteTpe</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadRouteTypeByKey)]
        public async Task<IActionResult> ReadRouteTypeByKey(int OID)
        {
            try
            {
                if (OID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var routeTypeInDbInDb = await context.RouteTypeRepository.GetRouteTypeByKey(OID);

                if (routeTypeInDbInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(routeTypeInDbInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/route-type/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="routeType">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateRouteType)]
        public async Task<IActionResult> UpdateRouteType(int key, RouteType routeType)
        {
            try
            {
                if (key != routeType.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsRouteTypeDuplicate(routeType) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.RouteTypeRepository.Update(routeType);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/route-type/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteRouteType)]
        public async Task<IActionResult> DeleteRouteType(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var routeTypeInDb = await context.RouteTypeRepository.GetRouteTypeByKey(key);

                if (routeTypeInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                routeTypeInDb.IsDeleted = true;

                context.RouteTypeRepository.Update(routeTypeInDb);
                await context.SaveChangesAsync();

                return Ok(routeTypeInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the routeType is duplicate?
        /// </summary>
        /// <param name="routeType">RouteType object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsRouteTypeDuplicate(RouteType routeType)
        {
            try
            {
                var routeTypeInDb = await context.RouteTypeRepository.GetRouteTypeByName(routeType.RouteTypeName);

                if (routeTypeInDb != null)
                    if (routeTypeInDb.OID != routeType.OID)
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