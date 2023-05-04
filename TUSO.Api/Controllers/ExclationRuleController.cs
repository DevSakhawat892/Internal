using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Api.Controllers
{
    /// <summary>
    ///ExclationRule Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class ExclationRuleController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="context"></param>
        public ExclationRuleController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/exclation-rule
        /// </summary>
        /// <param name="exclationRule">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateExclationRule)]
        public async Task<IActionResult> CreateExclationRule(ExclationRule exclationRule)
        {
            try
            {
                context.ExclationRuleRepository.Add(exclationRule);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadExclationRuleByKey", new { key = exclationRule.OID }, exclationRule);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/exclation-rules
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadExclationRules)]
        public async Task<IActionResult> ReadExclationRules()
        {
            try
            {
                var exclationRule = await context.ExclationRuleRepository.GetExclationRules();
                return Ok(exclationRule);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/exclation-rule/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table ExclationRules</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadExclationRuleByKey)]
        public async Task<IActionResult> ReadExclationRuleByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var exclationRule = await context.ExclationRuleRepository.GetExclationRuleByKey(key);

                if (exclationRule == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(exclationRule);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/exclation-rule/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <param name="exclationRule">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateExclationRule)]
        public async Task<IActionResult> UpdateExclationRule(int key, ExclationRule exclationRule)
        {
            try
            {
                if (key != exclationRule.OID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.ExclationRuleRepository.Update(exclationRule);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/exclation-rule/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteExclationRule)]
        public async Task<IActionResult> DeleteExclationRule(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var exclationRuleInDb = await context.ExclationRuleRepository.GetExclationRuleByKey(key);

                if (exclationRuleInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                exclationRuleInDb.IsDeleted = true;

                context.ExclationRuleRepository.Update(exclationRuleInDb);
                await context.SaveChangesAsync();

                return Ok(exclationRuleInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}