using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    public class RecoveryRequest : BaseModel
    {
        /// <summary>
        /// Primary Key of the table RecoveryRequests.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Description of the password recovery request. 
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string RequestDescription { get; set; }

        /// <summary>
        /// Recovery request status.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Status")]
        public bool Status { get; set; }

        /// <summary>
        /// Change password by admin.
        /// </summary>
        public long? ChangedPasswordBy { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table UserAccount table.
        /// </summary>
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserAccount UserAccount { get; set; }
    }
}