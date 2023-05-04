using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    public class Designation : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OID { get; set; }

        [Required(ErrorMessage = "Required!")]
        [StringLength(20)]
        public string DesignationName { get; set; }

        public string? Description { get; set; }

        public bool IsDepertmentHead { get; set; }

        /// <summary>
        ///  Foreign key. Primary key of entity Department.
        /// </summary>
        public int DepartmentID { get; set; }
      
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        /// <summary>
        /// UserAccounts of a Designation.
        /// </summary>
        public virtual IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}