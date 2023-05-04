using TUSO.Domain.Entities;
using TUSO.Infrastructure.Repositories;

/*
 * Created by: Emon
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface ISLARepository : IRepository<SLA>
    {
        /// <summary>
        /// Returns a SLA if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table SLA</param>
        /// <returns>Instance of a SLA object.</returns>
        public Task<SLA> GetSlaByKey(long OID);

        /// <summary>
        /// Returns all module.
        /// </summary>
        /// <returns>List of Module object.</returns>
        public Task<IEnumerable<SLA>> ReadSLAs();
    }
}
