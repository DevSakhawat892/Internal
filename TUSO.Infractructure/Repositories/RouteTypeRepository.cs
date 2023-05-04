using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Emon
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Repositories
{
    public class RouteTypeRepository : Repository<RouteType>, IRouteTypeRepository
    {
        public RouteTypeRepository(DataContext context) : base(context)
        {

        }

        public async Task<RouteType> GetRouteTypeByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.OID == OID && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RouteType> GetRouteTypeByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.RouteTypeName.ToLower().Trim() == name.ToLower().Trim() && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RouteType>> GetRouteTypes()
        {
            try
            {
                return await QueryAsync(i => i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}