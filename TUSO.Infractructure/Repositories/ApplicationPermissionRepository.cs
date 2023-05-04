using System.Reflection;
using System.Security.Cryptography;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Rakib, Bithy
 * Date created: 04.09.2022, 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
   public class ApplicationPermissionRepository : Repository<ApplicationPermission>, IApplicationPermissionRepository
   {
      public ApplicationPermissionRepository(DataContext context) : base(context)
      {

      }

      public async Task<ApplicationPermission> GetApplicationPermissionByKey(int OID)
      {
         try
         {
            return await FirstOrDefaultAsync(p => p.OID == OID && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<ApplicationPermission> GetApplicationPermissionByRole(int RoleID)
      {
         try
         {
            return await FirstOrDefaultAsync(p => p.RoleID == RoleID && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<ApplicationPermission> GetApplicationPermissionByModule(int ModuleID)
      {
         try
         {
            return await FirstOrDefaultAsync(p => p.ModuleID == ModuleID && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<ApplicationPermission> GetApplicationPermission(int RoleID, int ModuleID)
      {
         try
         {
            return await FirstOrDefaultAsync(p => p.RoleID == RoleID && p.ModuleID == ModuleID && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public List<ApplicationPermissionDto> GetApplicationPermissions()
      {
         var data = (from a in context.ApplicationPermissions
                           join m in context.Modules on a.ModuleID equals m.OID
                           join r in context.Roles on a.RoleID equals r.OID
                           select new
                           {
                              OID = a.OID,
                              RoleID = a.RoleID,
                              RoleName = r.RoleName,
                              ModuleID = a.ModuleID,
                              ModuleName = m.ModuleName,
                              ReadPermission = a.ReadPermission,
                              CreatePermission = a.CreatePermission,
                              EditPermission = a.EditPermission,
                              DeletePermission = a.DeletePermission,
                              AllData = 0
                           }).ToList();


         List<ApplicationPermissionDto> dto = new List<ApplicationPermissionDto>();
         foreach (var item in data)
         {
            dto.Add(new ApplicationPermissionDto {
               OID = item.OID,
               RoleID = item.RoleID,
               RoleName = item.RoleName,
               ModuleID = item.ModuleID,
               ModuleName = item.ModuleName,
               ReadPermission = item.ReadPermission,
               CreatePermission = item.CreatePermission,
               EditPermission = item.EditPermission,
               DeletePermission = item.DeletePermission,
               AllData = item.AllData
            });
         }

         return dto;

         //try
         //{
         //   return await QueryAsync(w => w.IsDeleted == false, i => i.Roles, n => n.Modules);
         //}
         //catch (Exception)
         //{
         //   throw;
         //}
      }
   }
}