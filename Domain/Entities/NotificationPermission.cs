using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 06.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    public class NotificationPermission : BaseModel
    {
        /// <summary>
        /// Primary Key of the table ConfigureNotifications.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// RoleID of the UserAccounts.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// PreviewID of the incidents.
        /// </summary>
        public long? PreviewID { get; set; }

        /// <summary>
        /// Foreign key. Primary key of entity Notification.
        /// </summary>
        public long NotificationID { get; set; }
        [ForeignKey("NotificationID")]
        public virtual Notification Notifications { get; set; }
    }
}