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
    /// TicketRouting Entity.
    /// </summary>
    public class TicketRouting : BaseModel
    {
        /// <summary>
        /// Primary key of the table TicketRouting.
        /// </summary>
        [Key]
        public long OID { get; set; }

        public long? CallCenterID { get; set; }

        public DateTime? CallCenterRefferDate { get; set; }

        public int? DepartmentID { get; set; }

        public DateTime? DepartmentRefferDate { get; set; }

        public long? AssignTo { get; set; }

        public DateTime? AssignDate { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity RouteType. 
        /// </summary>
        public int RouteTypeID { get; set; }

        [ForeignKey("RouteTypeID")]
        public virtual RouteType RouteTypes { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Incident. 
        /// </summary>
        public long IncidentID { get; set; }

        [ForeignKey("IncidentID")]
        public virtual Incident Incidents { get; set; }
    }
}