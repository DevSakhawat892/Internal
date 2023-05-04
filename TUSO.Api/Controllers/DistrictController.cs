using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022,17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public DistrictController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/district
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateDistrict)]
        public async Task<IActionResult> CreateDistrict(District district)
        {
            try
            {
                if (await IsDistrictDuplicate(district) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.DistrictRepository.Add(district);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadDistrictByKey", new { key = district.OID }, district);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/districts
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDistrict)]
        public async Task<IActionResult> ReadDistricts()
        {
            try
            {
                var district = await context.DistrictRepository.GetDistricts();
                return Ok(district);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/district/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Countries</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDistrictByKey)]
        public async Task<IActionResult> ReadDistrictByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var district = await context.DistrictRepository.GetDistrictByKey(key);

                if (district == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(district);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/district/province?ProvinceID={ProvinceID}
        /// </summary>
        /// <param name="ProvinceID">ProvinceID of province as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadDistrictByProvince)]
        public async Task<IActionResult> ReadDistrictByProvince(int ProvinceID)
        {
            try
            {
                if (ProvinceID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var permissionInDb = await context.DistrictRepository.GetDistrictByProvince(ProvinceID);

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
        /// URL: tuso-api/district/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="district">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateDistrict)]
        public async Task<IActionResult> UpdateDistrict(int key, District district)
        {
            try
            {
                if (key != district.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsDistrictDuplicate(district) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.DistrictRepository.Update(district);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/district/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteDistrict)]
        public async Task<IActionResult> DeleteDistrict(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var districtInDb = context.DistrictRepository.Get(key);

                if (districtInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                districtInDb.IsDeleted = true;

                context.DistrictRepository.Update(districtInDb);
                await context.SaveChangesAsync();

                return Ok(districtInDb);
            }
            catch (Exception)
            {
                ///WriteToLog(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the district name is duplicate?
        /// </summary>
        /// <param name="district">District object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsDistrictDuplicate(District district)
        {
            try
            {
                var districtInDb = await context.DistrictRepository.GetDistrictByName(district.DistrictName);

                if (districtInDb != null)
                    if (districtInDb.OID != district.OID)
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