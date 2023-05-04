using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Rakib, Bithy
 * Date created: 04.09.2022, 19.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// ApplicationPermission Entity.
    /// </summary>
    public class ApplicationPermission : BaseModel
    {
        /// <summary>
        /// Primary Key of the table Permission.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Retrive application permission specific role.
        /// </summary>        
        [Display(Name = "Read Permission")]
        public bool ReadPermission { get; set; }

        /// <summary>
        /// Create application permission specific role.
        /// </summary>
        [Display(Name = "Create Permission")]
        public bool CreatePermission { get; set; }

        /// <summary>
        /// Update application permission specific role.
        /// </summary>
        [Display(Name = "Edit Permission")]
        public bool EditPermission { get; set; }

        /// <summary>
        /// Delete application permission specific role.
        /// </summary>
        [Display(Name = "Delete Permission")]
        public bool DeletePermission { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Role.
        /// </summary>
        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Roles { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Module.
        /// </summary>
        public int ModuleID { get; set; }

        [ForeignKey("ModuleID")]
        public virtual Module Modules { get; set; }
    }
}