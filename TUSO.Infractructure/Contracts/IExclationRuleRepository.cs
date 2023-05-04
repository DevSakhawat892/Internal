using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IExclationRuleRepository : IRepository<ExclationRule>
    {
        /// <summary>
        /// Returns a exclationRule if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table ExclationRules</param>
        /// <returns>Instance of a ExclationRule object.</returns>
        public Task<ExclationRule> GetExclationRuleByKey(int OID);

        /// <summary>
        /// Returns all exclationRule.
        /// </summary>
        /// <returns>List of ExclationRule object.</returns>
        public Task<IEnumerable<ExclationRule>> GetExclationRules();
    }
}