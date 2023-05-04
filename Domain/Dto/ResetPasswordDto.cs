/*
 * Created by: Bithy
 * Date created: 04.09.2022
 * Last modified: 04.09.2022
 * Modified by: Bithy
 */
using System.ComponentModel.DataAnnotations;

namespace TUSO.Domain.Dto
{
    public class ResetPasswordDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
       
    }
}