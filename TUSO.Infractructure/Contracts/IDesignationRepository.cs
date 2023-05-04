using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Entities;

namespace TUSO.Infrastructure.Contracts
{
   public interface IDesignationRepository : IRepository<Designation>
   {
      /// <summary>
      /// Return a Designation if key match.
      /// </summary>
      /// <param name="key">primary key of the table.</param>
      /// <returns>Instance of a designation object.</returns>
      public Task<Designation> GetDesignationBykey(int key);

      /// <summary>
      /// Return a Designation if the name match.
      /// </summary>
      /// <param name="Name">Designation name of the user.</param>
      /// <returns>Instance of a Designation table object.</returns>
      public Task<Designation> GetDesignationByName(string Name);

      /// <summary>
      /// Returns a Designation if DepartmentID matched
      /// </summary>
      /// <param name="DepartmentID"></param>
      /// <returns></returns>
      public Task<IEnumerable<Designation>> GetDesignationByDepartment(int DepartmentID);

      /// <summary>
      /// Return all Designation.
      /// </summary>
      /// <returns>List of Designation</returns>
      public Task<IEnumerable<Designation>> GetDesignations();
   }
}