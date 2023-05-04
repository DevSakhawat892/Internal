using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 01.09.2022, 10.09.2022, 17.09.2022 
 * Modified by: Sakhawat, Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Facility constructor.
        /// </summary>
        /// <param name="context"></param>
        public FacilityController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/facility
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateFacility)]
        public async Task<IActionResult> CreateFacility(Facility facility)
        {
            try
            {
                if (await IsFacilityDuplicate(facility) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.FacilityRepository.Add(facility);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadFacilityByKey", new { key = facility.OID }, facility);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/facilities
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadFacilities)]
        public async Task<IActionResult> ReadFacilities()
        {
            try
            {
                var facilityInDb = await context.FacilityRepository.GetFacilities();
                return Ok(facilityInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/province/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Countries</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadFacilityByKey)]
        public async Task<IActionResult> ReadProvinceByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var facilityInDb = await context.FacilityRepository.GetFacilityByKey(key);

                if (facilityInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(facilityInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/facility/district?DistrictID={DistrictID}
        /// </summary>
        /// <param name="DistrictID">DistrictID of district as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadFacilityByDistrict)]
        public async Task<IActionResult> ReadFacilityByDistrict(int DistrictID)
        {
            try
            {
                if (DistrictID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.FacilityRepository.GetFacilityByDistrict(DistrictID);

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
        /// URL: tuso-api/facility/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="facility">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateFacility)]
        public async Task<IActionResult> UpdateFacility(int key, Facility facility)
        {
            try
            {
                if (key != facility.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsFacilityDuplicate(facility) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.FacilityRepository.Update(facility);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/facility/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteFacility)]
        public async Task<IActionResult> DeleteFacility(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var facilityInDb = await context.FacilityRepository.GetFacilityByKey(key);

                if (facilityInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                facilityInDb.IsDeleted = true;

                context.FacilityRepository.Update(facilityInDb);
                await context.SaveChangesAsync();

                return Ok(facilityInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the facility is duplicate?
        /// </summary>
        /// <param name="facility">Facility object</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsFacilityDuplicate(Facility facility)
        {
            try
            {
                var userfacilityInDb = await context.FacilityRepository.GetFacilityByName(facility.FacilityName);

                if (userfacilityInDb != null)
                    if (userfacilityInDb.OID != facility.OID)
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