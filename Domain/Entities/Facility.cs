using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Utilities.Constants;

/*
 * Created by: Rezwana, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Facility Entity.
    /// </summary>
    public class Facility : BaseModel
    {
        /// <summary>
        /// Primary Key of the table Facility.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Facility.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the entity District.
        /// </summary>
        public int DistrictID { get; set; }

        [ForeignKey("DistrictID")]
        public virtual District Districts { get; set; }
    }
}