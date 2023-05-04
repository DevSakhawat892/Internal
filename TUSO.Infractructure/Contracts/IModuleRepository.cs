using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 04.09.2022
 * Last modified: 04.09.2022, 10.09.2022
 * Modified by: Sakhawat, Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IModuleRepository : IRepository<Module>
    {
        /// <summary>
        /// Returns a module if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table Module</param>
        /// <returns>Instance of a Module object.</returns>
        public Task<Module> GetModuleByKey(int OID);

        /// <summary>
        /// Returns a module if the module name matched.
        /// </summary>
        /// <param name="name">Name of Module</param>
        /// <returns>Instance of a module table object.</returns>
        public Task<Module> GetModuleByName(string name);

        /// <summary>
        /// Returns all module.
        /// </summary>
        /// <returns>List of Module object.</returns>
        public Task<IEnumerable<Module>> GetModules();
    }
}