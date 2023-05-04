using TUSO.Domain.Entities;

/*
 * Created by: Labib
 * Date created: 31.08.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
   public interface ICountryRepository : IRepository<Country>
   {
      /// <summary>
      /// Returns a country if key matched.
      /// </summary>
      /// <param name="key">Primary key of the table Countries</param>
      /// <returns>Instance of a Country object.</returns>
      public Task<Country> GetCountryByKey(int OID);

      /// <summary>
      /// Returns a country if the name matched.
      /// </summary>
      /// <param name="countryName">Country name of the user.</param>
      /// <returns>Instance of a Country object.</returns>
      public Task<Country> GetCountryByName(string countryName);

      /// <summary>
      /// Returns all country.
      /// </summary>
      /// <returns>List of Country object.</returns>
      public Task<IEnumerable<Country>> GetCountries();
   }
}