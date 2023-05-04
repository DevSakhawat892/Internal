/*
 * Created by: Bithy
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Dto
{
    public class UserAccountDto
    {
        public long OID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CountryCode { get; set; }
        public string Cellphone { get; set; }
        public bool IsAccountActive { get; set; }
        public int CountryID { get; set; }        
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int DistrictID { get; set; }
        public int FacilityID { get; set; }
        public int DesignationID { get; set; }
        public int RoleID { get; set; }
    }
}