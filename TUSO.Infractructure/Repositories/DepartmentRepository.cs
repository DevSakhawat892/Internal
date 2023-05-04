using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Emon
 * Date created: 13.09.2022
 * Last modified: 13.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext context) : base(context)
        {

        }

        public async Task<Department> GetDepartmentByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.OID == OID && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Department> GetDepartmentByName(string departmentName)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.DepartmentName.ToLower().Trim() == departmentName.ToLower().Trim() && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                return await QueryAsync(c => c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}