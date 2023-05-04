using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 31.08.2022
 * Last modified: 31.08.2022, 10.09.2022
 * Modified by: Sakhawat, Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public ProvinceRepository(DataContext context) : base(context)
        {

        }

        public async Task<Province> GetProvinceByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(p => p.OID == OID && p.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Province> GetProvinceByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(p => p.ProvinceName.ToLower().Trim() == name.ToLower().Trim() && p.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Province>> GetProvinceByCountry(int CountryID)
        {
            try
            {
                return await QueryAsync(p => p.CountryID == CountryID && p.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<Province> GetProvinceByCountry(int CountryID)
        //{
        //    var data = context.Provinces.Where(w => w.OID == CountryID).ToList();
        //    return data;
        //}

        public async Task<IEnumerable<Province>> GetProvinces()
        {
            try
            {
                return await QueryAsync(w => w.IsDeleted == false, i=> i.Countries);                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}