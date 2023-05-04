using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 07.09.2022
 * Last modified: 07.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    /// <summary>
    ///ProfilePicture Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class ProfilePictureController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="context"></param>
        public ProfilePictureController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/profile-picture
        /// </summary>
        /// <param name="profilePicture">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateProfilePicture)]
        public async Task<IActionResult> CreateProfilePicture(ProfilePicture profilePicture)
        {
            try
            {
                context.ProfilePictureRepository.Add(profilePicture);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadProfilePictureByKey", new { key = profilePicture.OID }, profilePicture);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/profile-pictures
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProfilePictures)]
        public async Task<IActionResult> ReadProfilePictures()
        {
            try
            {
                var profilePicture = await context.ProfilePictureRepository.GetProfilePictures();
                return Ok(profilePicture);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/profile-picture/key/{OID}
        /// </summary>
        /// <param name="OID">Primary key of the table ProfilePictures</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProfilePictureByKey)]
        public async Task<IActionResult> ReadProfilePictureByKey(long OID)
        {
            try
            {
                if (String.IsNullOrEmpty(OID.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccount = await context.ProfilePictureRepository.GetProfilePictureByKey(OID);

                if (userAccount == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/profile-picture/user?UserID={UserID}
        /// </summary>
        /// <param name="UserID">UserID of UserAccount as parameter</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProfilePictureByUser)]
        public async Task<IActionResult> ReadProfilePictureByUser(long UserID)
        {
            try
            {
                if (UserID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var profileInDb = await context.ProfilePictureRepository.GetProfilePictureByUser(UserID);

                if (profileInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(profileInDb);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/profile-picture/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="profilePicture">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateProfilePicture)]
        public async Task<IActionResult> UpdateProfilePicture(long key, ProfilePicture profilePicture)
        {
            try
            {
                if (key != profilePicture.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.ProfilePictureRepository.Update(profilePicture);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/profile-picture/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteProfilePicture)]
        public async Task<IActionResult> DeleteProfilePicture(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var profilePictureInDb = await context.ProfilePictureRepository.GetProfilePictureByKey(key);

                if (profilePictureInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                profilePictureInDb.IsDeleted = true;

                context.ProfilePictureRepository.Update(profilePictureInDb);
                await context.SaveChangesAsync();

                return Ok(profilePictureInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}