using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 31.08.2022
 * Modified by: Rakib
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Preview Entity. 
    /// </summary>
    public class Preview : BaseModel
    {
        /// <summary>
        /// Primary key of the entity Preview.
        /// </summary>
        [Key]
        public long PreviewID { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table UserAccount.
        /// </summary>
        public long UserAccountID { get; set; }
        
        /// <summary>
        /// Foreign Key. Primary key of the table IncidentType.
        /// </summary>
        public int IncidentTypeID { get; set; }
        
    }
}