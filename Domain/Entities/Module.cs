using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Module Entity.
    /// </summary>
    public class Module : BaseModel
    {
        /// <summary>
        /// Primary key of the table Module.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Module
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Description of the Module.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// ApplicationPermissions of a Module.
        /// </summary>
        public virtual IEnumerable<ApplicationPermission> ApplicationPermissions { get; set; }
    }
}