using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat, Bithy
 * Date created: 31.08.2022, 10.09.2022
 * Last modified: 31.08.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Role Entity.
    /// </summary>
    public class Role : BaseModel
    {
        /// <summary>
        /// Primary key of the table Role.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Role.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        /// <summary>
        /// Description of the role.
        /// </summary>       
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// UserAccounts of a Role.
        /// </summary>
        public virtual IEnumerable<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// ApplicationPermissions of a Role.
        /// </summary>
        public virtual IEnumerable<ApplicationPermission> ApplicationPermissions { get; set; }
    }
}