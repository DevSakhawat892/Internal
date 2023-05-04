using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    public class Department : BaseModel
    {
        /// <summary>
        /// Primary key of the DepartmentInfo table
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OID { get; set; }

        /// <summary>
        /// Name of the Department
        /// </summary>
        [Required(ErrorMessage = "Required!")]
        [StringLength(20)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Purpose of the Department
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Designations of a Department.
        /// </summary>
        public virtual IEnumerable<Designation> Designations { get; set; }

        /// <summary>
        /// TierLevels of a Department.
        /// </summary>
        public virtual IEnumerable<TierLevel> TierLevels { get; set; }
    }
}