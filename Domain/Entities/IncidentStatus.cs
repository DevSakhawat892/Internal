using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    /// IncidentStatus Entity.
    /// </summary>
    public class IncidentStatus : BaseModel
    {
        // <summary>
        /// Primary key of the table IncidentStatus.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Comments of the IncidentStatus.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Comments")]
        public string Comment { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table Incident.
        /// </summary>
        public long IncidentID { get; set; }

        [ForeignKey("IncidentID")]
        public virtual Incident Incidents { get; set; }
    }
}