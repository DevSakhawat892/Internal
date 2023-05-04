using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 05.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        /// <summary>
        /// Returns a assignment if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table Assignments</param>
        /// <returns>Instance of a Assignment object.</returns>
        public Task<Assignment> GetAssignmentByKey(long OID);

        /// <summary>
        /// Returns all assignment.
        /// </summary>
        /// <returns>List of Assignment object.</returns>
        public Task<IEnumerable<Assignment>> GetAssignments();
    }
}