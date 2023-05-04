using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Domain.Validators;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 03.09.2022, 10.09.2022
 * Modified by: Rakib, Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// UserAccount Entity.
    /// </summary>
    public class UserAccount : BaseModel
    {
        /// <summary>
        /// Primary Key of the table UserAccounts.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Name of the user.
        /// </summary>       
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(61)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [IfNotAlphabet]
        public string Name { get; set; }

        /// <summary>
        /// Surname of the user.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [IfNotAlphabet]
        public string Surname { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        [StringLength(90)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [IfNotEmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Username of the account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        /// <summary>
        /// Login password.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Password)]
        [Display(Name = "Login Password")]
        public string Password { get; set; }

        /// <summary>
        /// Country Code of the cellphone.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(5)]
        [DataType(DataType.Text)]
        [Display(Name = "Country Code")]
        [IfNotValidCountryCode]
        public string CountryCode { get; set; }

        /// <summary>
        /// Cellphone number.
        /// </summary>        
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(15)]
        [DataType(DataType.Text)]
        [Display(Name = "Cellphone")]
        [IfNotInteger]
        public string Cellphone { get; set; }

        /// <summary>
        /// Useraccount's status active or inactive.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Active Status")]
        public bool IsAccountActive { get; set; }

        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int FacilityID { get; set; }

        /// <summary>
        /// Foreign key. Primary key of entity Designation.
        /// </summary>
        public int DesignationID { get; set; }

        [ForeignKey("DesignationID")]
        public virtual Designation Designations { get; set; }

        /// <summary>
        /// Foreign key. Primary key of entity Role.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [IfNotSelected]
        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Roles { get; set; }

        /// <summary>
        /// GeographicPermissions of a UserAccount.
        /// </summary>
        public virtual IEnumerable<GeographicPermission> GeographicPermissions { get; set; }

        public virtual IEnumerable<RecoveryRequest> RecoveryRequests { get; set; }
    }
}