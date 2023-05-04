using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 22.09.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// RouteType Entity.
    /// </summary>
    public class RouteType : BaseModel
    {
        /// <summary>
        /// Primary key of the table RouteType.
        /// </summary>
        [Key]
        public int OID { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "Route type Name")]
        public string RouteTypeName { get; set; }

        /// <summary>
        /// TicketRoutings of a RouteType.
        /// </summary>
        public virtual IEnumerable<TicketRouting> TicketRoutings { get; set; }
    }
}