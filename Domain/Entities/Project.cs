using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Domain.Entities
{
    /// <summary>
    /// Project Entity.
    /// </summary>
    public class Project : BaseModel
    {
        // <summary>
        /// Primary key of the table Project.
        /// </summary>
        [Key]
        public int OID { get; set; }

        /// <summary>
        /// Title of the project.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        public string Title { get; set; }

        /// <summary>
        /// Purpose of the project
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(500)]
        public string Description { get; set; }
    }
}