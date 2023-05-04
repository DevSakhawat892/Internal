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
    /// ProfilePicture Entity.
    /// </summary>
    public class ProfilePicture : BaseModel
    {
        // <summary>
        /// Primary key of the table ProfilePicture.
        /// </summary>
        [Key]
        public long OID { get; set; }

        /// <summary>
        /// User profile picture
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public Byte[] ProfilePictures { get; set; }
    }
}