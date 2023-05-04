using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 13.09.2022
 * Last modified: 13.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Contracts
{

    public interface IDepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// Returns a department if the name matched.
        /// </summary>
        /// <param name="departmentName">Department name of the user.</param>
        /// <returns>Instance of a Department object.</returns>
        public Task<Department> GetDepartmentByKey(int OID);

        /// <summary>
        /// Returns a department if the name matched.
        /// </summary>
        /// <param name="departmentName">Department name of the user.</param>
        /// <returns>Instance of a Department object.</returns>
        public Task<Department> GetDepartmentByName(string departmentName);

        /// <summary>
        /// Returns all department.
        /// </summary>
        /// <returns>List of Department object.</returns>
        public Task<IEnumerable<Department>> GetDepartments();
    }
}