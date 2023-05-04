using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 13.09.2022
 * Last modified: 13.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Infrastructure.Repositories
{
    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public DesignationRepository(DataContext context) : base(context)
        {
        }

        public async Task<Designation> GetDesignationBykey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.OID == key && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Designation> GetDesignationByName(string Name)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.DesignationName.ToLower().Trim() == Name.ToLower().Trim() && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Designation>> GetDesignationByDepartment(int DepartmentID)
        {
            try
            {
                return await QueryAsync(d => d.DepartmentID == DepartmentID && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Designation>> GetDesignations()
        {
            try
            {
                return await QueryAsync(w => w.IsDeleted == false, i => i.Department);
                //return await QueryAsync(w => w.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}