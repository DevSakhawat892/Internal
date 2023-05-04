using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;

/*
 * Created by: Labib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Country Entity.
    /// </summary>
    public class Country : BaseModel
    {
        /// <summary>
        /// Primary key of the table Country.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Country.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        /// <summary>
        /// Provinces of a Country.
        /// </summary>
        public virtual IEnumerable<Province> Provinces { get; set; }
    }
}