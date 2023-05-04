using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Infrastructure.Contracts
{
   public interface ITierLevelRepository : IRepository<TierLevel>
   {
      /// <summary>
      /// Returns a TierLevel if key matched.
      /// </summary>
      /// <param name="OID">Primary key of the table TierLevel</param>
      /// <returns>Instance of a TierLevel object.</returns>
      public Task<TierLevel> GetTierLevelByKey(int OID);

      /// <summary>
      /// Return a TierLevel if the name match.
      /// </summary>
      /// <param name="Name">TierLevel name of the user.</param>
      /// <returns>Instance of a TierLevel table object.</returns>
      public Task<TierLevel> GetTierLevelByName(string name);

      /// <summary>
      /// Return all TierLevel.
      /// </summary>
      /// <returns>List of TierLevel</returns>
      public Task<IEnumerable<TierLevel>> GetTierLevels();
   }
}