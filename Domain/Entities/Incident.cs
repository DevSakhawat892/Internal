using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TUSO.Domain.Validators;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Incident Entity.
    /// </summary>
    public class Incident : BaseModel
    {
        /// <summary>
        /// Primary key of the table Incident.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// DateReported of an Incident.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date Reported")]
        [DateValidator]
        public DateTime DateReported { get; set; }

        /// <summary>
        /// Title of an Incident.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        // <summary>
        /// Description of an Incident.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //[MaxLength(2000)]
        //[Display(Name = "Screenshot")]
        //public byte[]? Screenshot { get; set; }

        /// <summary>
        ///  DueDate of an Incident.
        /// </summary>       
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Due Date")]
        [DateValidator]
        public DateTime? DueDate { get; set; }

        /// <summary>
        ///  DateResolved of an Incident.
        /// </summary>       
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date Resolved")]
        public DateTime? DateResolved { get; set; }

        /// <summary>
        /// Incident status of the Incident.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Incident status")]
        public int IncidentStatus { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table UserAccount.
        /// </summary>
        public long UserAccountID { get; set; }

        [ForeignKey("UserAccountID")]
        public virtual UserAccount UserAccounts { get; set; }

        /// <summary>
        /// Foreign Key. Primary key of the table IncidentType.
        /// </summary>
        public int IncidentTypeID { get; set; }

        /// <summary>
        /// Assignments of a UserAccount.
        /// </summary>
        public virtual IEnumerable<Assignment> Assignments { get; set; }

        /// <summary>
        /// IncidentStatus of a UserAccount.
        /// </summary>
        public virtual IEnumerable<IncidentStatus> IncidentStatuses { get; set; }

        /// <summary>
        /// TicketRoutings of a Incident.
        /// </summary>
        public virtual IEnumerable<TicketRouting> TicketRoutings { get; set; }
    }
}