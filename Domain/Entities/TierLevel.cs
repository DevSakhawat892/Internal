using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Bithy
 * Date created: 22.09.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// TierLevel Entity.
    /// </summary>
    public class TierLevel : BaseModel
    {
        /// <summary>
        /// Primary key of the table TierLevel.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Name of the Tier level.
        /// </summary>
        public string TierName { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Department. 
        /// </summary>
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department Departments { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity IncidentType. 
        /// </summary>
        public int IncidentTypeID { get; set; }

        [ForeignKey("IncidentTypeID")]
        public virtual IncidentType IncidentTypes { get; set; }
    }
}