using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 05.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IIncidentStatusRepository : IRepository<IncidentStatus>
    {
        /// <summary>
        /// Returns a IncidentStatus if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table IncidentStatus</param>
        /// <returns>Instance of a IncidentStatus object.</returns>
        public Task<IncidentStatus> GetIncidentStatusByKey(long OID);

        /// <summary>
        /// Returns a incidentStatus if the incidentStatus name matched.
        /// </summary>
        /// <param name="name">Name of incidentStatus</param>
        /// <returns>Instance of a incidentStatus table object.</returns>
        public Task<IncidentStatus> GetIncidentStatusByName(string name);

        /// <summary>
        /// Returns all incidentStatus.
        /// </summary>
        /// <returns>List of IncidentStatus object.</returns>
        public Task<IEnumerable<IncidentStatus>> GetIncidentStatuses();
    }
}