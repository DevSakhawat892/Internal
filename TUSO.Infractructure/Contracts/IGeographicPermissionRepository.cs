using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified:10.09.2022, 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IGeographicPermissionRepository : IRepository<GeographicPermission>
    {
        /// <summary>
        /// Returns a grographic permission if OID matched
        /// </summary>
        /// <param name="key"Primary key of the table ></param>
        /// <returns></returns>
        public Task<GeographicPermission> GetGeographicPermissionByKey(int OID);

        /// <summary>
        /// Returns a grographic permission if UserAccountID matched
        /// </summary>
        /// <param name="UserAccountID"Primary key of the useraccount table ></param>
        /// <returns></returns>
        public Task<GeographicPermission> GetGeographicPermissionByUser(int UserAccountID);

        /// <summary>
        /// Returns a grographic permission if ProvinceID matched
        /// </summary>
        /// <param name="ProvinceID"Primary key of the province table></param>
        /// <returns></returns>
        public Task<GeographicPermission> GetGeographicPermissionByProvince(int ProvinceID);

        /// <summary>
        /// Returns all grographic permission.
        /// </summary>
        /// <returns>List of grographic permission object.</returns>
        public Task<IEnumerable<GeographicPermission>> GetGeographicPermissions();
    }
}