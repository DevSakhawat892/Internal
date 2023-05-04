using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 15.09.2022
 * Last modified: 15.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Infrastructure.Contracts
{
   public interface IIncidentCategoryRepository : IRepository<IncidentCategory>
   {
      /// <summary>
      /// Return a IncidentCategory if key match.
      /// </summary>
      /// <param name="key">Primary key of the table.</param>
      /// <returns>Instance of a IncidentCategory Table object.</returns>
      public Task<IncidentCategory> GetIncidentCategoryByKey(int key);

      /// <summary>
      /// Return a IncidentCategory if name match.
      /// </summary>
      /// <param name="name">Category of incident.</param>
      /// <returns>Instance of IncidentCateogry object.</returns>
      public Task<IncidentCategory> GetIncidentCategoryByName(string name);

      /// <summary>
      /// Return all IncidentCateogory.
      /// </summary>
      /// <returns></returns>
      public Task<IEnumerable<IncidentCategory>> GetIncidentCategories();
   }
}