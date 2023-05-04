using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Labib, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(DataContext context) : base(context)
        {

        }

        public async Task<District> GetDistrictByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.OID == OID && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<District> GetDistrictByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.DistrictName.ToLower().Trim() == name.ToLower().Trim() && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<District>> GetDistrictByProvince(int ProvinceID)
        {
            try
            {
                return await QueryAsync(d => d.ProvinceID == ProvinceID && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<District>> GetDistricts()
        {
            try
            {
                return await QueryAsync(d => d.IsDeleted == false, i => i.Provinces);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}