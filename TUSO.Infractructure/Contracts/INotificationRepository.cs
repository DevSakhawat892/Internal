using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface INotificationRepository : IRepository<Notification>
    {
        /// <summary>
        /// Returns a notification if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table Notifications</param>
        /// <returns>Instance of a Notification object.</returns>
        public Task<Notification> GetNotificationByKey(int OID);

        /// <summary>
        /// Returns all notification.
        /// </summary>
        /// <returns>List of Notification object.</returns>
        public Task<IEnumerable<Notification>> GetNotifications();

        bool MarkAllAsRead(long key);
        bool MarkAsRead(long notid);
    }
}