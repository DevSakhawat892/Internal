using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Labib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext context) : base(context)
        {

        }

        public async Task<Role> GetRoleByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(r => r.OID == OID && r.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Role> GetRoleByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(r => r.RoleName.ToLower().Trim() == name.ToLower().Trim() && r.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            try
            {
                return await QueryAsync(r => r.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}