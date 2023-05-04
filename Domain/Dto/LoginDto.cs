using System.ComponentModel.DataAnnotations;
using TUSO.Utilities.Constants;
/*
 * Created by: Bithy
 * Date created: 04.09.2022
 * Last modified: 04.09.2022, 11.09.2022
 * Modified by: Bithy,Rakib
 */
namespace TUSO.Domain.Dto
{
    /// <summary>
    /// DTO or View model for account login.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Username for login an account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        public string UserName { get; set; }

        /// <summary>
        /// Password for login an account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}