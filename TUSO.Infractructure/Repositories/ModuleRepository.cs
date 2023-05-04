using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 04.09.2022
 * Last modified: 04.09.2022, 10.09.2022
 * Modified by: Sakhawat, Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(DataContext context) : base(context)
        {

        }

        public async Task<Module> GetModuleByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(m => m.OID == OID && m.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Module> GetModuleByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(m => m.ModuleName.ToLower().Trim() == name.ToLower().Trim() && m.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Module>> GetModules()
        {
            try
            {
                return await QueryAsync(m => m.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}