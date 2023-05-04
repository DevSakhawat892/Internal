using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface ITicketRoutingRepository : IRepository<TicketRouting>
    {
        /// <summary>
        /// Returns a TicketRouting if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table TicketRouting</param>
        /// <returns>Instance of a TicketRouting object.</returns>
        public Task<TicketRouting> GetTicketRoutingByKey(int OID);

        /// <summary>
        /// Returns all TicketRouting.
        /// </summary>
        /// <returns>List of TicketRouting object.</returns>
        public Task<IEnumerable<TicketRouting>> GetTicketRoutings();
    }
}