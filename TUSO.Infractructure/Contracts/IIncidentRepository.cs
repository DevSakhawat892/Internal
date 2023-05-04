using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 05.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IIncidentRepository : IRepository<Incident>
    {
        /// <summary>
        /// Returns a Incident if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table Incident</param>
        /// <returns>Instance of a Incident object.</returns>
        public Task<Incident> GetIncidentByKey(long OID);

        /// <summary>
        /// Returns a module if the module name matched.
        /// </summary>
        /// <param name="name">Name of Module</param>
        /// <returns>Instance of a module table object.</returns>
        public Task<Incident> GetIncidentByName(string name);

        /// <summary>
        /// Returns all module.
        /// </summary>
        /// <returns>List of Module object.</returns>
        public Task<IEnumerable<Incident>> GetIncidents();
    }
}