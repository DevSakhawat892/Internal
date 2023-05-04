using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Assignment Entity. 
    /// </summary>
    public class Assignment : BaseModel
    {
        /// <summary>
        /// Primary key of the entity Assignment.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table UserAccount.
        /// </summary>
        public long UserAccountID { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table Incident.
        /// </summary>
        public long IncidentID { get; set; }

        [ForeignKey("IncidentID")]
        public virtual Incident Incidents { get; set; }
    }
}