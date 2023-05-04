using System.ComponentModel.DataAnnotations;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    public class Notification : BaseModel
    {
        /// <summary>
        ///  Primary Key of the table Notification.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Description of the notification.
        /// </summary>
        public string NotificationDescription { get; set; }

        /// <summary>
        /// Type of the notification.
        /// </summary>
        public string NotificationType { get; set; }

        /// <summary>
        /// Return url.
        /// </summary>
        public string ReturnURL { get; set; }

        /// <summary>
        /// Is Notification read or not?
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Notification of a ConfigureNotification.
        /// </summary>
        public virtual IEnumerable<Notification> Notifications { get; set; }
    }
}