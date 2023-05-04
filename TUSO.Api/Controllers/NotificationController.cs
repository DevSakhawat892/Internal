using Microsoft.AspNetCore.Mvc;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 06.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Api.Controllers
{
    /// <summary>
    /// Notification controller.
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public NotificationController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/notifications
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadNotifications)]
        public async Task<IActionResult> ReadNotifications()
        {
            try
            {
                var notification = await context.NotificationRepository.GetNotifications();
                return Ok(notification);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/notification/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Notifications</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadNotificationByKey)]
        public async Task<IActionResult> ReadNotificationByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var notification = await context.NotificationRepository.GetNotificationByKey(key);

                if (notification == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(notification);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        #region Mark  all as read
        /// <summary>
        /// tuso-api/notification/key/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route(RouteConstants.MarkAllAsRead)]
        //public async Task<IActionResult> MarkAllAsRead(long key)
        //{
        //    try
        //    {
        //        var Return = context.NotificationRepository.MarkAllAsRead(key);

        //        return Ok(Return);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
        #endregion

        #region Mark as read for single notification
        /// <summary>
        /// tuso-api/notification/key/{notid}
        /// </summary>
        /// <param name="notid"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route(RouteConstants.MarkAsRead)]
        //public async Task<IActionResult> MarkAsRead(long notid)
        //{
        //    try
        //    {
        //        var Return = context.NotificationRepository.MarkAsRead(notid);

        //        return Ok(Return);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
        #endregion
    }
}