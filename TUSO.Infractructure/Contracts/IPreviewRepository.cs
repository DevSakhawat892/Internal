using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 05.09.2022
 * Last modified: 05.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IPreviewRepository : IRepository<Preview>
    {
        /// <summary>
        /// Returns a preview if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table Previews</param>
        /// <returns>Instance of a Preview object.</returns>
        public Task<Preview> GetPreviewByKey(long key);

        /// <summary>
        /// Returns all preview.
        /// </summary>
        /// <returns>List of Preview object.</returns>
        public Task<IEnumerable<Preview>> GetPreviews();
    }
}