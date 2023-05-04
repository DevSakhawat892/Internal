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
    /// SLA Entity.
    /// </summary>
    public class SLA : BaseModel
    {
        /// <summary>
        /// Primary key of the table SLA.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Description of the SLA.
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        /// <summary>
        /// MaximumTime of SLA.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Maximum Time")]
        public int MaximumTime { get; set; }

        /// <summary>
        /// WithHoliday of SLA.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "With Holiday")]
        public bool WithHoliday { get; set; }

        /// <summary>
        /// WithWorkingHour of SLA.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "With Working Hour")]
        public bool WithWorkingHour { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Holiday. 
        /// </summary>
        public int HolidayID { get; set; }

        [ForeignKey("HolidayID")]
        public virtual Holiday? Holidays { get; set; }

        /// <summary>
        /// Priorities of a SLA.
        /// </summary>
        public virtual IEnumerable<Priority> Priorities { get; set; }

        /// <summary>
        /// ExclationRules of a SLA.
        /// </summary>
        public virtual IEnumerable<ExclationRule> ExclationRules { get; set; }
    }
}