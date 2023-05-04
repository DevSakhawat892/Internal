using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// IncidentType Entity.
    /// </summary>
    public class IncidentType : BaseModel
    {
        // <summary>
        /// Primary key of the table IncidentType.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the IncidentType.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Incident Type")]
        public string IncidentTypeName { get; set; }

        /// <summary>
        /// Name of the IncidentType.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// ParentID of IncidentType.
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// TierLevels of a IncidentType.
        /// </summary>
        public virtual IEnumerable<TierLevel> TierLevels { get; set; }
    }
}