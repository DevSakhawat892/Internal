using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 05.09.2022
 * Last modified: 05.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class PreviewRepository : Repository<Preview>, IPreviewRepository
    {
        public PreviewRepository(DataContext context) : base(context)
        {

        }

        public async Task<Preview> GetPreviewByKey(long key)
        {
            try
            {
                return await FirstOrDefaultAsync(p => p.PreviewID == key && p.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Preview>> GetPreviews()
        {
            try
            {
                return await QueryAsync(p => p.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}