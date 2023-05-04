using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IProfilePictureRepository : IRepository<ProfilePicture>
    {
        /// <summary>
        /// Returns a profilePicture if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table ProfilePictures</param>
        /// <returns>Instance of a ProfilePicture object.</returns>
        public Task<ProfilePicture> GetProfilePictureByKey(long OID);

        public Task<ProfilePicture> GetProfilePictureByUser(long UserID);

        /// <summary>
        /// Returns all profilePicture.
        /// </summary>
        /// <returns>List of ProfilePicture object.</returns>
        public Task<IEnumerable<ProfilePicture>> GetProfilePictures();
    }
}