using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        public FacilityRepository(DataContext context) : base(context)
        {

        }

        public async Task<Facility> GetFacilityByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(f => f.OID == OID && f.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Facility> GetFacilityByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(f => f.FacilityName.ToLower().Trim() == name.ToLower().Trim() && f.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Facility>> GetFacilityByDistrict(int DistrictID)
        {
            try
            {
                return await QueryAsync(d => d.DistrictID == DistrictID && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Facility>> GetFacilities()
        {
            try
            {
                return await QueryAsync(f => f.IsDeleted == false, i => i.Districts);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}