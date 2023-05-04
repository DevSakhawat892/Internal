using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// HolidayList Entity.
    /// </summary>
    public class HolidayList : BaseModel
    {
        /// <summary>
        /// Primary key of the table HolidayList.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Holiday  date
        /// </summary>
        [Required(ErrorMessage = "Holiday date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Holiday { get; set; }

        /// <summary>
        /// Define day name
        /// </summary>
        [Required(ErrorMessage = "Dayname is required.")]
        [StringLength(30)]
        public string DayName { get; set; }

        /// <summary>
        /// Purpose of holidays
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100)]
        public string Discription { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Holiday. 
        /// </summary>
        public int HolidayID { get; set; }

        [ForeignKey("HolidayID")]
        public virtual Holiday? Holidays { get; set; }
    }
}