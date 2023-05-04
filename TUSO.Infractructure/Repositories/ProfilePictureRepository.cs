using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class ProfilePictureRepository : Repository<ProfilePicture>, IProfilePictureRepository
    {
        public ProfilePictureRepository(DataContext context) : base(context)
        {

        }

        public async Task<ProfilePicture> GetProfilePictureByKey(long OID)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.OID == OID && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProfilePicture> GetProfilePictureByUser(long UserID)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.OID == UserID && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProfilePicture>> GetProfilePictures()
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