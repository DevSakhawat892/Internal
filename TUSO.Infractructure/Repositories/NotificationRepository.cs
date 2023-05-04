using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext context) : base(context)
        {

        }

        public async Task<Notification> GetNotificationByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.OID == OID && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            try
            {
                return await QueryAsync(c => c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool MarkAllAsRead(long key)
        {
            bool res = false;
            //var data = context.Notifications.Where(w => w.Userid == key && w.IsRead == false).ToList();
            //if (data.Count > 0)
            //{
            //    foreach (var i in data)
            //    {
            //        i.Isread = true;
            //        context.SaveChanges();
            //    }
            //    res = true;
            //}
            return res;
        }

        public bool MarkAsRead(long notid)
        {
            bool res = false;
            var data = context.Notifications.Where(w => w.OID == notid).FirstOrDefault();
            if (data != null)
            {
                data.IsRead = true;
                context.SaveChanges();
                res = true;
            }
            return res;
        }
    }
}