using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 10.09.2022
 * Last modified: 10.09.2022, 17.09.2022 
 * Modified by: Sakhawat, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class RecoveryRequestController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public RecoveryRequestController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/recovery-request
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateRecoveryRequest)]
        public async Task<IActionResult> CreateRequestRecovery(RecoveryRequestDto recoveryRequestDto)
        {
            try
            {
                var user = await context.UserAccountRepository.GetUserAccountByName(recoveryRequestDto.UserName);

                if (user != null)
                {
                    RecoveryRequest recoveryRequest = new RecoveryRequest
                    {
                        UserId = user.OID,
                        RequestDescription = recoveryRequestDto.RequestDescription,
                        Status = true
                    };

                    context.RecoveryRequestRepository.Add(recoveryRequest);
                    await context.SaveChangesAsync();
                    return CreatedAtAction("ReadRecoveryRequestByKey", new { key = recoveryRequest.OID }, recoveryRequestDto);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URl: tuso-api/recovery-requests
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadRecoveryRequests)]
        public async Task<IActionResult> ReadRecoveryRequests()
        {
            try
            {
                var recoveryRequestInDb = await context.RecoveryRequestRepository.GetRecoveryRequests();
                return Ok(recoveryRequestInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL : tuso-api/recovery-request/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table RecoveryRequest</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadRecoveryRequestByKey)]
        public async Task<IActionResult> ReadRecoveryRequestByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var recoveryRequestInDb = await context.RecoveryRequestRepository.GetRecoveryRequestByKey(key);

                if (recoveryRequestInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(recoveryRequestInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/recovery-request/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="recoveryRequest">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateRecoveryRequest)]
        public async Task<IActionResult> UpdateRecoveryRequest(int key, RecoveryRequest recoveryRequest)
        {
            try
            {
                if (key != recoveryRequest.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.RecoveryRequestRepository.Update(recoveryRequest);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/recovery-request/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteRecoveryRequest)]
        public async Task<IActionResult> DeleteRecoveryRequest(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var recoveryRequestInDb = await context.RecoveryRequestRepository.GetRecoveryRequestByKey(key);

                if (recoveryRequestInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                recoveryRequestInDb.IsDeleted = true;

                context.RecoveryRequestRepository.Update(recoveryRequestInDb);
                await context.SaveChangesAsync();

                return Ok(recoveryRequestInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

    }
}
