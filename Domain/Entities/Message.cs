using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 14.09.2022
 * Last modified: 14.09.2022, 14.09.2022
 * Modified by: Sakhawat, Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Message Entity.
    /// </summary>
    public class Message : BaseModel
    {
        /// <summary>
        /// Primary key of the table Message.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// Message Date date of the row.
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Message Date")]
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Name of the Message.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(200)]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Messages { get; set; }

        /// <summary>
        /// Name of the Message.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(91)]
        [DataType(DataType.Text)]
        [Display(Name = "Sender")]
        public string Sender { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity Incident. 
        /// </summary>
        public long IncidentID { get; set; }

        [ForeignKey("IncidentID")]
        public virtual Incident Incident { get; set; }
    }
}