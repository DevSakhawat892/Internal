using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 10.09.2022, 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class GeographicPermissionRepository : Repository<GeographicPermission>, IGeographicPermissionRepository
    {
        public GeographicPermissionRepository(DataContext context) : base(context)
        {

        }

        public async Task<GeographicPermission> GetGeographicPermissionByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(g => g.OID == OID && g.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeographicPermission> GetGeographicPermissionByUser(int UserAccountID)
        {
            try
            {
                return await FirstOrDefaultAsync(g => g.UserAccountID == UserAccountID && g.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeographicPermission> GetGeographicPermissionByProvince(int ProvinceID)
        {
            try
            {
                return await FirstOrDefaultAsync(g => g.ProvinceID == ProvinceID && g.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GeographicPermission>> GetGeographicPermissions()
        {
            try
            {
                return await QueryAsync(g => g.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}