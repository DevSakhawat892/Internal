using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib, Bithy
 * Date created: 31.08.2022, 03.09.2022
 * Last modified: 10.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUnitOfWork context;
        //private readonly IWebHostEnvironment webHostEnvironment;
        public UserAccountsController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/user-account
        /// </summary>
        /// <param name="userAccount">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateUserAccount)]
        public async Task<IActionResult> CreateUserAccount(UserAccountDto userAccount)
        {
            try
            {
                //if (await IsAccountDuplicate(userAccount) == true)
                //   return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

                UserAccount user = new UserAccount
                {
                    OID = userAccount.OID,
                    Name = userAccount.Name,
                    Surname = userAccount.Surname,
                    Username = userAccount.Username,
                    CountryID = userAccount.CountryID,
                    ProvinceID = userAccount.ProvinceID,
                    DistrictID = userAccount.DistrictID,
                    FacilityID = userAccount.FacilityID,
                    DesignationID = userAccount.DesignationID,
                    RoleID = userAccount.RoleID,
                    Email = userAccount.Email,
                    CountryCode = userAccount.CountryCode,
                    Cellphone = userAccount.Cellphone,
                    Password = userAccount.Password,
                    IsAccountActive = false,
                    DateCreated = DateTime.Now
                };

                context.UserAccountRepository.Add(user);
                await context.SaveChangesAsync();

                GeographicPermission geographic = new GeographicPermission();
                geographic.ProvinceID = userAccount.ProvinceID;
                geographic.DistrictID = userAccount.DistrictID;
                geographic.FacilityID = userAccount.FacilityID;
                geographic.UserAccountID = user.OID;

                context.GeographicPermissionRepository.Add(geographic);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadUserAccountByKey", new { key = userAccount.OID }, userAccount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/user-accounts
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccounts)]
        public async Task<IActionResult> ReadUserAccounts()
        {
            try
            {
                var userAccounts = await context.UserAccountRepository.GetUsers();
                return Ok(userAccounts);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/user-account/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Countries</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccountByKey)]
        public async Task<IActionResult> ReadUserAccountByKey(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccount = await context.UserAccountRepository.GetUserAccountByKey(key);

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
        /// URL: tuso-api/user-account/key/{username}
        /// </summary>
        /// <param name="key">Primary key of the table Countries</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccountByUsername)]
        public async Task<IActionResult> ReadUserAccountByUsername(string username)
        {
            try
            {
                if (username == null)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccount = await context.UserAccountRepository.GetUserAccountByName(username);

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
        /// URL: tuso-api/user-account/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="userAccount">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateUserAccount)]
        public async Task<IActionResult> UpdateUserAccount(long key, UserAccount userAccount)
        {
            try
            {
                if (key != userAccount.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsAccountDuplicate(userAccount) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

                context.UserAccountRepository.Update(userAccount);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/user-account/login
        /// </summary>
        //[HttpPost]
        //[Route(RouteConstants.UserLogin)]
        //public async Task<IActionResult> UserLogin(LoginDto login)
        //{
        //   try
        //   {
        //      var user = context.UserAccountRepository.GetUserByUserNamePassword(login.UserName, login.Password);

        //      if (user.Result != null)
        //      {
        //         if (await IsAccountDuplicate(login) == true)
        //            return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

        //         context.UserAccountRepository.Add(user);
        //          await context.SaveChangesAsync();

        //         // context.GeographicPermissionRepository.Add();
        //          return CreatedAtAction("ReadUserAccountByKey", new { key = userAccount.OID }, userAccount);
        //      }
        //      else
        //      {
        //         return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
        //      }
        //   }
        //   catch (Exception ex)
        //   {
        //      return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
        //   }
        //}

        /// <summary>
        /// URL: tuso-api/user-account/changepassword
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.ChangedPassword)]
        public async Task<IActionResult> ChangedPassword(ResetPasswordDto changePassword)
        {
            try
            {
                var check = await context.UserAccountRepository.GetUserByUserNamePassword(changePassword.UserName, changePassword.Password);

                if (check.Password == changePassword.Password)
                {
                    if (check != null)
                    {
                        check.Password = changePassword.NewPassword;
                        context.UserAccountRepository.Update(check);
                        await context.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/user-account/details/{key}
        /// </summary>
        /// <param name="key">Primary key of the table UserAccounts</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.UserDetails)]
        public async Task<IActionResult> UserDetails(long key)
        {
            try
            {
                if (key == 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userInDb = await context.UserAccountRepository.GetAllUserDetail(key);

                if (userInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(userInDb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// URL: tuso-api/user-account/recovery-request
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.RecoveryRequest)]
        public async Task<IActionResult> RecoveryRequest(RecoveryRequestDto recoveryRequestDto)
        {
            try
            {
                var userInfo = await context.UserAccountRepository.GetUserAccountByKey(recoveryRequestDto.UserId);
                var recoveryRequest = await context.RecoveryRequestRepository.GetRecoveryRequestByKey(recoveryRequestDto.OID);

                if (recoveryRequest != null)
                {
                    if (userInfo.Password != recoveryRequestDto.Password)
                    {
                        userInfo.Password = recoveryRequestDto.Password;
                        context.UserAccountRepository.Update(userInfo);

                        recoveryRequest.Status = false;
                        recoveryRequest.ChangedPasswordBy = recoveryRequestDto.ChangedPasswordBy;
                        context.RecoveryRequestRepository.Update(recoveryRequest);

                        await context.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/user-account/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteUserAccount)]
        public async Task<IActionResult> DeleteUserAccount(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccountInDb = await context.UserAccountRepository.GetUserAccountByKey(key);

                if (userAccountInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                userAccountInDb.IsDeleted = true;

                context.UserAccountRepository.Update(userAccountInDb);
                await context.SaveChangesAsync();

                return Ok(userAccountInDb);
            }
            catch (Exception ex)
            {
                ///WriteToLog(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// Checks whether the user account is duplicate? 
        /// </summary>
        /// <param name="userAccount">UserAccount object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsAccountDuplicate(UserAccount userAccount)
        {
            try
            {
                var userAccountInDb = await context.UserAccountRepository.GetUserAccountByName(userAccount.Username);

                if (userAccountInDb != null)
                    if (userAccountInDb.OID != userAccount.OID)
                        return true;

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}