using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 19.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// GeographicPermission Entity.
    /// </summary>
    public class GeographicPermission : BaseModel
    {
        // <summary>
        /// Primary key of the table GeographicPermissions.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table Province.
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table District.
        /// </summary>
        public int DistrictID { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table Facility.
        /// </summary>
        public int FacilityID { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table UserAccount.
        /// </summary>
        public long UserAccountID { get; set; }

        [ForeignKey("UserAccountID")]
        public virtual UserAccount UserAccount { get; set; }
    }
}