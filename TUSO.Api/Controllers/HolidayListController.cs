using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022, 21.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Api.Controllers
{
    /// <summary>
    ///HolidayList Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class HolidayListController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="context"></param>
        public HolidayListController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/holidaylist
        /// </summary>
        /// <param name="holidayList">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateHolidayList)]
        public async Task<IActionResult> CreateHolidayList(HolidayList holidayList)
        {
            try
            {
                context.HolidayListRepository.Add(holidayList);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadHolidayListByKey", new { key = holidayList.OID }, holidayList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holidaylist-post
        /// </summary>
        /// <param name="holidayList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.CreateAllHolidayList)]
        public string CreateAllHolidayList(HolidayList holidayList)
        {
            try
            {
                context.HolidayListRepository.PostAllHolidayList(holidayList);
                context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }

        /// <summary>
        /// URL: tuso-api/holidaylist-vacation
        /// </summary>
        /// <param name="holiday">Object to be saved in the table as a row.</param>
        /// <param name="froms"></param>
        /// <param name="tos"></param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateVacation)]
        public async Task<IActionResult> CreateVacation(HolidayList holiday, DateTime froms, DateTime tos)
        {
            try
            {
                var holidayInDb = new HolidayList
                {
                    OID = holiday.OID,
                    DayName = holiday.DayName,
                    Discription = holiday.Discription,
                    Holiday = holiday.Holiday,
                    HolidayID = 1
                };

                context.HolidayListRepository.GetVacation(holidayInDb, froms, tos);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// URL: tuso-api/holidaylists
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadHolidayLists)]
        public async Task<IActionResult> ReadHolidayLists()
        {
            try
            {
                var holidayList = await context.HolidayListRepository.GetHolidayLists();
                return Ok(holidayList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holidaylist/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table HolidayLists</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadHolidayListByKey)]
        public async Task<IActionResult> ReadHolidayListByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var holidayList = await context.HolidayListRepository.GetHolidayListByKey(key);

                if (holidayList == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(holidayList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/holidaylist/holiday?HolidayID={HolidayID}
        /// </summary>
        /// <param name="HolidayID">HolidayID of holiday as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadHolidayListByHoliday)]
        public async Task<IActionResult> ReadHolidayListByHoliday(int HolidayID)
        {
            try
            {
                if (HolidayID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var holidayListInDb = await context.HolidayListRepository.GetHolidayListByHoliday(HolidayID);

                if (holidayListInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(holidayListInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holidaylist/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="holidayList">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateHolidayList)]
        public async Task<IActionResult> UpdateHolidayList(int key, HolidayList holidayList)
        {
            try
            {
                if (key != holidayList.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.HolidayListRepository.Update(holidayList);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/holidaylist/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteHolidayList)]
        public async Task<IActionResult> DeleteHolidayList(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var holidayListInDb = await context.HolidayListRepository.GetHolidayListByKey(key);

                if (holidayListInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                holidayListInDb.IsDeleted = true;

                context.HolidayListRepository.Update(holidayListInDb);
                await context.SaveChangesAsync();

                return Ok(holidayListInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}