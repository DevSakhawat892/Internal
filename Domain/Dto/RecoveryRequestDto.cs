namespace TUSO.Domain.Dto
{
    public class RecoveryRequestDto
    {
        public int OID { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string RequestDescription { get; set; }
        public bool Status { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public long ChangedPasswordBy { get; set; }
    }
}