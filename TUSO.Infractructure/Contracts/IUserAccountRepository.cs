using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        /// <summary>
        /// Returns a user account if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table UserAccounts</param>
        /// <returns>Instance of a UserAccount object.</returns>
        public Task<UserAccount> GetUserAccountByKey(long OID);

        /// <summary>
        /// Returns a user account if the username matched.
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>Instance of a UserAccount object.</returns>
        public Task<UserAccount> GetUserAccountByName(string name);

        /// <summary>
        /// Returns all active user accounts.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>List of UserAccount object.</returns>
        public Task<UserDetailDto> GetAllUserDetail(long key);

        /// <summary>
        /// Returns all active user accounts.
        /// </summary>
        /// <returns>List of UserAccount object.</returns>
        public Task<IEnumerable<UserAccount>> GetUsers();

        /// <summary>
        /// Returns a user account if the username,password matched.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>Instance of a UserAccount object.</returns>
        public Task<UserAccount> GetUserByUserNamePassword(string UserName, string Password);
    }
}