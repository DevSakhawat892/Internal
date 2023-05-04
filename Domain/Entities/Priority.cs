using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Priority Entity.
    /// </summary>
    public class Priority : BaseModel
    {
        /// <summary>
        /// Primary key of the table Priority.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Priority.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "Priority")]
        public string PriorityName { get; set; }

        /// <summary>
        /// Duration of the priority.
        /// </summary>   
        public int Duration { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity SLA. 
        /// </summary>
        public int SLAID { get; set; }

        [ForeignKey("SLAID")]
        public virtual SLA? SLA { get; set; }
    }
}