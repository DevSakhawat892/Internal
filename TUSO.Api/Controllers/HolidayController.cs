using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Api.Controllers
{
    /// <summary>
    ///Holiday Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="context"></param>
        public HolidayController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/holiday
        /// </summary>
        /// <param name="holiday">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateHoliday)]
        public async Task<IActionResult> CreateHoliday(Holiday holiday)
        {
            try
            {
                if (await IsHolidayDuplicate(holiday) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.HolidayRepository.Add(holiday);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadHolidayByKey", new { key = holiday.OID }, holiday);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holidays
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadHolidays)]
        public async Task<IActionResult> ReadHolidays()
        {
            try
            {
                var holiday = await context.HolidayRepository.GetHolidays();
                return Ok(holiday);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holiday/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Holidays</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadHolidayByKey)]
        public async Task<IActionResult> ReadHolidayByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var country = await context.HolidayRepository.GetHolidayByKey(key);

                if (country == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(country);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holiday/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="holiday">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateHoliday)]
        public async Task<IActionResult> UpdateHoliday(int key, Holiday holiday)
        {
            try
            {
                if (key != holiday.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsHolidayDuplicate(holiday) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                context.HolidayRepository.Update(holiday);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holiday/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteHoliday)]
        public async Task<IActionResult> DeleteHoliday(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var holidayInDb = await context.HolidayRepository.GetHolidayByKey(key);

                if (holidayInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                holidayInDb.IsDeleted = true;

                context.HolidayRepository.Update(holidayInDb);
                await context.SaveChangesAsync();

                return Ok(holidayInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the holiday name is duplicate? 
        /// </summary>
        /// <param name="holiday">Holiday object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsHolidayDuplicate(Holiday holiday)
        {
            try
            {
                var holidayInDb = await context.HolidayRepository.GetHolidayByName(holiday.HolidayName);

                if (holidayInDb != null)
                    if (holidayInDb.OID != holiday.OID)
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