using System.ComponentModel.DataAnnotations;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Holiday Entity.
    /// </summary>
    public class Holiday : BaseModel
    {
        /// <summary>
        /// Primary key of the table Holiday.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Holiday.
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Holiday Name")]
        public string? HolidayName { get; set; }

        /// <summary>
        /// Holiday start date
        /// </summary>
        [Required(ErrorMessage = "Holiday start-date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Holiday end date
        /// </summary>
        [Required(ErrorMessage = "Holiday end-date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// HolidayLists of a Holiday.
        /// </summary>
        public virtual IEnumerable<HolidayList> HolidayLists { get; set; }

        /// <summary>
        /// SLAs of a Holiday.
        /// </summary>
        public virtual IEnumerable<SLA> SLAs { get; set; }
    }
}