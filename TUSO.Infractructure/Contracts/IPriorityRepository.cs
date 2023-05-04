using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IPriorityRepository : IRepository<Priority>
    {
        /// <summary>
        /// Return a Priority if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table Priority.</param>
        /// <returns>Instance of Priority Table.</returns>
        public Task<Priority> GetPriorityByKey(int key);

        /// <summary>
        /// Retrun a Priority if name match.
        /// </summary>
        /// <param name="name">Priority of the Incident</param>
        /// <returns>Instance of the table Priority.</returns>
        public Task<Priority> GetPriorityByName(string name);

        /// <summary>
        /// Return all Priority.
        /// </summary>
        /// <returns>List of Priority object.</returns>
        public Task<IEnumerable<Priority>> GetPriorities();
    }
}