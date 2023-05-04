using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 05.09.2022
 * Last modified: 05.09.2022, 17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly IUnitOfWork context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public PreviewController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL: tuso-api/preview
        /// </summary>
        /// <param name="preview">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [Route(RouteConstants.CreatePreview)]
        public async Task<IActionResult> CreatePreview(Preview preview)
        {
            try
            {
                context.PreviewRepository.Add(preview);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadPreviewByKey", new { key = preview.PreviewID }, preview);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/previews
        /// </summary>
        /// <returns>List of table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadPreviews)]
        public async Task<IActionResult> ReadPreviews()
        {
            try
            {
                var preview = await context.PreviewRepository.GetPreviews();
                return Ok(preview);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/preview/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Previews</param>
        /// <returns>Instance of a table object.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadPreviewByKey)]
        public async Task<IActionResult> ReadPreviewByKey(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var preview = await context.PreviewRepository.GetPreviewByKey(key);

                if (preview == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(preview);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/preview/{key}
        /// </summary>
        /// <param name="key">Primary key of the talbe</param>
        /// <param name="preview">Object to be updated</param>
        /// <returns>Update row in the table.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdatePreview)]
        public async Task<IActionResult> UpdatePreview(long key, Preview preview)
        {
            try
            {
                if (key != preview.PreviewID)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                context.PreviewRepository.Update(preview);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: tuso-api/preview/{key}
        /// </summary>
        /// <param name="key">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeletePreview)]
        public async Task<IActionResult> DeletePreview(long key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var previewInDb = await context.PreviewRepository.GetPreviewByKey(key);

                if (previewInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                previewInDb.IsDeleted = true;

                context.PreviewRepository.Update(previewInDb);
                await context.SaveChangesAsync();

                return Ok(previewInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}