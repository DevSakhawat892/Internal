using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Utilities.Constants;

/*
 * Created by: Labib, Bithy
 * Date created: 31.08.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// District Entity.
    /// </summary>
    public class District : BaseModel
    {
        /// <summary>
        /// Primary key of the table District.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the District.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Province. 
        /// </summary>
        public int ProvinceID { get; set; }

        [ForeignKey("ProvinceID")]
        public virtual Province Provinces { get; set; }

        /// <summary>
        /// Facilities of a District.
        /// </summary>
        public virtual IEnumerable<Facility> Facilities { get; set; }
    }
}