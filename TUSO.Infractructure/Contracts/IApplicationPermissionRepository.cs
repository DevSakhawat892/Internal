using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

/*
 * Created by: Rakib, Bithy
 * Date created: 04.09.2022, 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
   public interface IApplicationPermissionRepository : IRepository<ApplicationPermission>
   {
      /// <summary>
      /// Returns a application permission if key matched
      /// </summary>
      /// <param name="key"Primary key of the table ></param>
      /// <returns></returns>
      public Task<ApplicationPermission> GetApplicationPermissionByKey(int OID);

      /// <summary>
      /// Returns a application permission if RoleID matched
      /// </summary>
      /// <param name="RoleID"Primary key of the role table ></param>
      /// <returns></returns>
      public Task<ApplicationPermission> GetApplicationPermissionByRole(int RoleID);

      /// <summary>
      /// Returns a application permission if ModuleID matched
      /// </summary>
      /// <param name="ModuleID"Primary key of the module table></param>
      /// <returns></returns>
      public Task<ApplicationPermission> GetApplicationPermissionByModule(int ModuleID);

      /// <summary>
      /// Returns a application permission if RoleID, ModuleID matched
      /// </summary>
      /// <param name="RoleID">Primary key of the role table</param>
      /// <param name="ModuleID">Primary key of the module table</param>
      /// <returns></returns>
      public Task<ApplicationPermission> GetApplicationPermission(int RoleID, int ModuleID);

      /// <summary>
      /// Returns all Application permission.
      /// </summary>
      /// <returns>List of Application permission object.</returns>
      public List<ApplicationPermissionDto> GetApplicationPermissions();
      //public Task<IEnumerable<ApplicationPermission>> GetApplicationPermissions();
   }
}