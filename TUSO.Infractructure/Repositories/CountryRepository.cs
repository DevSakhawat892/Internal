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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base(context)
        {

        }

        public async Task<Country> GetCountryByKey(int OID)
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

        public async Task<Country> GetCountryByName(string countryName)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.CountryName.ToLower().Trim() == countryName.ToLower().Trim() && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Country>> GetCountries()
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
    }
}