using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IProvinceRepository : IRepository<Province>
    {
        /// <summary>
        /// Returns a province if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table Province</param>
        /// <returns>Instance of a Province object.</returns>
        public Task<Province> GetProvinceByKey(int OID);

        /// <summary>
        /// Returns a province if the province name matched.
        /// </summary>
        /// <param name="name">Province name of the user</param>
        /// <returns>Instance of a province table object.</returns>
        public Task<Province> GetProvinceByName(string name);

        /// <summary>
        /// Returns a province if CountryID matched
        /// </summary>
        /// <param name="CountryID"Primary key of the country table ></param>
        /// <returns></returns>
        public Task<IEnumerable<Province>> GetProvinceByCountry(int CountryID);

        /// <summary>
        /// Returns all province.
        /// </summary>
        /// <returns>List of Province object.</returns>
        public Task<IEnumerable<Province>> GetProvinces();
    }
}