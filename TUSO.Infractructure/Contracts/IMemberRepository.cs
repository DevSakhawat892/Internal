using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Entities;

namespace TUSO.Infrastructure.Contracts
{
   public interface IMemberRepository : IRepository<Member>
   {
      /// <summary>
      /// Returns a member if key match.
      /// </summary>
      /// <param name="key">Primary key of the table.</param>
      /// <returns>Instance of a Member object.</returns>
      public Task<Member> GetMemberByKey(int key);

      /// <summary>
      /// Return all Member.
      /// </summary>
      /// <returns>List of member object.</returns>
      public Task<IEnumerable<Member>> GetMembers();
   }
}
