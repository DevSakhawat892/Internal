
namespace TUSO.Domain.Dto
{
    public class UserDetailDto
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
        public string Country { get; set; }
        public string? Province { get; set; }
        public string? ProvinceName { get; set; }
        public string? District { get; set; }
        public string? Facility { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
    }
}