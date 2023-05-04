using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IRouteTypeRepository : IRepository<RouteType>
    {
        /// <summary>
        /// Returns a RouteType if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table RouteType</param>
        /// <returns>Instance of a RouteType object.</returns>
        public Task<RouteType> GetRouteTypeByKey(int OID);

        /// <summary>
        /// Returns a routeType if the routeType name matched.
        /// </summary>
        /// <param name="name">Type of route</param>
        /// <returns>Instance of a routeType table object.</returns>
        public Task<RouteType> GetRouteTypeByName(string name);

        /// <summary>
        /// Returns all RouteType.
        /// </summary>
        /// <returns>List of RouteType object.</returns>
        public Task<IEnumerable<RouteType>> GetRouteTypes();
    }
}