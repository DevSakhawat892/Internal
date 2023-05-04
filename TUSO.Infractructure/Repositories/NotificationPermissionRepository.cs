using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 06.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class NotificationPermissionRepository : Repository<NotificationPermission>, INotificationPermissionRepository
    {
        public NotificationPermissionRepository(DataContext context) : base(context)
        {

        }
    }
}