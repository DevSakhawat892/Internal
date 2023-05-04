using TUSO.Domain.Entities;

/*
 * Created by: Labib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IIncidentTypeRepository : IRepository<IncidentType>
    {
        /// <summary>
        /// Returns a IncidentType if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table IncidentType</param>
        /// <returns>Instance of a IncidentType object.</returns>
        public Task<IncidentType> GetIncidentTypeByKey(int OID);

        /// <summary>
        /// Returns a incidentType if the incidentType name matched.
        /// </summary>
        /// <param name="name">Type of incident</param>
        /// <returns>Instance of a ncidentType table object.</returns>
        public Task<IncidentType> GetIncidentTypeByName(string name);

        /// <summary>
        /// Returns all IncidentType.
        /// </summary>
        /// <returns>List of IncidentType object.</returns>
        public Task<IEnumerable<IncidentType>> GetIncidentTypes();
    }
}