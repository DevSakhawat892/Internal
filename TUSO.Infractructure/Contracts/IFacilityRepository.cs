using TUSO.Domain.Entities;

/*
 * Created by: Labib, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        /// <summary>
        /// Returns a facility if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table Facility</param>
        /// <returns>Instance of a Facility object.</returns>
        public Task<Facility> GetFacilityByKey(int OID);

        /// <summary>
        /// Returns a user facility if the facility name matched.
        /// </summary>
        /// <param name="name">Facility name of the user</param>
        /// <returns>Instance of a facility table object.</returns>
        public Task<Facility> GetFacilityByName(string name);

        /// <summary>
        /// Returns a facility if DistrictID matched
        /// </summary>
        /// <param name="DistrictID"></param>
        /// <returns></returns>
        public Task<IEnumerable<Facility>> GetFacilityByDistrict(int DistrictID);

        /// <summary>
        /// Returns all facility.
        /// </summary>
        /// <returns>List of Facility object.</returns>
        public Task<IEnumerable<Facility>> GetFacilities();
    }
}