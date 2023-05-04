using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Rakib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022,17.09.2022 
 * Modified by: Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
   /// <summary>
   ///Country Controller
   /// </summary>
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class CountryController : ControllerBase
   {
      private readonly IUnitOfWork context;

      /// <summary>
      ///Default constructor
      /// </summary>
      /// <param name="context"></param>
      public CountryController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: tuso-api/country
      /// </summary>
      /// <param name="entity">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [HttpPost]
      [Route(RouteConstants.CreateCountry)]
      public async Task<IActionResult> CreateCountry(Country country)
      {
         try
         {
            if (await IsCountryDuplicate(country) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.CountryRepository.Add(country);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadCountryByKey", new { key = country.OID }, country);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/country
      /// </summary>
      /// <returns>List of table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadCountries)]
      public async Task<IActionResult> ReadCountries()
      {
         try
         {
            var country = await context.CountryRepository.GetCountries();
            return Ok(country);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/country/key/{key}
      /// </summary>
      /// <param name="key">Primary key of the table Countries</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadCountryByKey)]
      public async Task<IActionResult> ReadCountryByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var country = await context.CountryRepository.GetCountryByKey(key);

            if (country == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(country);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/country/{key}
      /// </summary>
      /// <param name="key">Primary key of the talbe</param>
      /// <param name="country">Object to be updated</param>
      /// <returns>Update row in the table.</returns>
      [HttpPut]
      [Route(RouteConstants.UpdateCountry)]
      public async Task<IActionResult> UpdateCountry(int key, Country country)
      {
         try
         {
            if (key != country.OID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsCountryDuplicate(country) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.CountryRepository.Update(country);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/country/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      [Route(RouteConstants.DeleteCountry)]
      public async Task<IActionResult> DeleteCountry(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var countryInDb = await context.CountryRepository.GetCountryByKey(key);

            if (countryInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            countryInDb.IsDeleted = true;

            context.CountryRepository.Update(countryInDb);
            await context.SaveChangesAsync();

            return Ok(countryInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Checks whether the country name is duplicate? 
      /// </summary>
      /// <param name="country">Country object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsCountryDuplicate(Country country)
      {
         try
         {
            var countryInDb = await context.CountryRepository.GetCountryByName(country.CountryName);

            if (countryInDb != null)
               if (countryInDb.OID != country.OID)
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