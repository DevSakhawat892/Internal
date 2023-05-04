using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(DataContext context) : base(context)
        {

        }

        public async Task<UserAccount> GetUserAccountByKey(long OID)
        {
            try
            {
                return await FirstOrDefaultAsync(u => u.OID == OID && u.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAccount> GetUserAccountByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(u => u.Username.ToLower().Trim() == name.ToLower().Trim() && u.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserAccount>> GetUsers()
        {
            try
            {
                return await QueryAsync(u => u.IsDeleted == false, i =>i.Roles, w =>w.Designations);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDetailDto> GetAllUserDetail(long key)
        {
            var data = (from u in context.UserAccounts.Where(w => w.OID == key)
                        join d in context.Designations on u.DesignationID equals d.OID
                        join c in context.Countries on u.CountryID equals c.OID
                        join r in context.Roles on u.RoleID equals r.OID
                        join e in context.Departments on d.DepartmentID equals e.OID
                        join p in context.Provinces on u.ProvinceID equals p.OID
                        join f in context.Facilities on u.FacilityID equals f.OID
                        select new
                        {
                            Userid = u.OID,
                            Name = u.Name,
                            Surname = u.Surname,
                            Username = u.Username,
                            Email = u.Email,
                            CountryCode = u.CountryCode,
                            Cellphone = u.Cellphone,
                            Status = u.IsAccountActive,

                            Desigid = d.OID,
                            Designationname = d.DesignationName,

                            Countryid = c.OID,
                            Country = c.CountryName,

                            Role = r.RoleName,
                            Roleid = r.OID,

                            Deptid = e.OID,
                            Departmentname = e.DepartmentName,

                            ProvinceID = p.OID,
                            ProvinceName = p.ProvinceName,

                            facilityId = f.OID,
                            FacilityName = f.FacilityName,
                        }).FirstOrDefault();


            UserDetailDto dto = new UserDetailDto();
            if (data != null)
            {
                bool st = false;
                if (data.Status != null)
                {
                    st = true;
                }

                dto.OID = data.Userid;
                dto.IsAccountActive = st;

                dto.Email = data.Email;
                dto.Cellphone = data.CountryCode + " " + data.Cellphone;
                dto.Country = data.Country;
                dto.Role = data.Role;
                dto.Designation = data.Designationname;
                dto.Province = data.ProvinceName;
                dto.Facility = data.FacilityName;
                dto.Username = data.Username;
                dto.Username = data.Name + " " + data.Surname;
            }
            return dto;
        }

        public async Task<UserAccount> GetUserByUserNamePassword(string UserName, string Password)
        {
            try
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Username == UserName && u.Password == Password && u.IsDeleted == false);

                if (user == null)
                    user = new UserAccount();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}