using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Infrastructure.Contracts
{
   public interface IRecoveryRequestRepository : IRepository<RecoveryRequest>
   {
      /// <summary>
      /// Returns a Recovery Request if key matched.
      /// </summary>
      /// <param name="key">Primary key of the table RecoveryRequest</param>
      /// <returns>Instance of a RecoveryRequest object.</returns>
      public Task<RecoveryRequest> GetRecoveryRequestByKey(int key);

      /// <summary>
      /// Returns a RecoveryRequest if the RecoveryRequest name matched.
      /// </summary>
      /// <param name="name">RecoveryRequest name of the user</param>
      /// <returns>Instance of a RecoveryRequest table object.</returns>
      public Task<RecoveryRequest> GetRecoveryRequestByName(string name);

      /// <summary>
      /// Returns all RecoveryRequest.
      /// </summary>
      /// <returns>List of RecoveryRequest object.</returns>
      public Task<IEnumerable<RecoveryRequest>> GetRecoveryRequests();
   }
}
