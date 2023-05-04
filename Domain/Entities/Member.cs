using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created by: Bithy
 * Date created: 14.09.2022
 * Last modified: 14.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    public class Member : BaseModel
    {
        /// <summary>
        /// Primary key of the table Member.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table UserAccount.
        /// </summary>
        public long UserAccountID { get; set; }

        [ForeignKey("UserAccountID")]
        public virtual UserAccount UserAccounts { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table Team.
        /// </summary>
        public long TeamID { get; set; }

        [ForeignKey("TeamID")]
        public virtual Team Teams { get; set; }

        /// <summary>
        /// Indicates is he/she team lead or not.
        /// </summary>
        [Display(Name = "Is Team Lead")]
        public bool IsTeamLead { get; set; }
    }
}