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
    /// ExclationRule Entity.
    /// </summary>
    public class ExclationRule : BaseModel
    {
        /// <summary>
        /// Primary key of the table ExclationRule.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Description of the ExclationRule.
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        /// <summary>
        /// RouteTo of ExclationRule
        /// </summary>
        public long RouteTo { get; set; }

        /// <summary>
        /// RouteGroup of ExclationRule
        /// </summary>
        public int RouteGroup { get; set; }

        /// <summary>
        /// Time of Route.
        /// </summary>
        public int RouteTime { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity SLA. 
        /// </summary>
        public int SLAID { get; set; }

        [ForeignKey("SLAID")]
        public virtual SLA? SLA { get; set; }
    }
}