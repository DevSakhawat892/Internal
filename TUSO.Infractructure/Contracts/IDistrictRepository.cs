using TUSO.Domain.Entities;

/*
 * Created by: Labib, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 04.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IDistrictRepository : IRepository<District>
    {
        /// <summary>
        /// Returns a district if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table Districts</param>
        /// <returns>Instance of a District object.</returns>
        public Task<District> GetDistrictByKey(int key);

        /// <summary>
        /// Returns a district if the name matched.
        /// </summary>
        /// <param name="name">District name of the user</param>
        /// <returns>Instance of a District object.</returns>
        public Task<District> GetDistrictByName(string name);

        /// <summary>
        /// Returns a province if ProvinceID matched
        /// </summary>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public Task<IEnumerable<District>> GetDistrictByProvince(int ProvinceID);

        /// <summary>
        /// Returns all district.
        /// </summary>
        /// <returns>List of District object.</returns>
        public Task<IEnumerable<District>> GetDistricts();
    }
}