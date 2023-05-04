using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

namespace TUSO.Infrastructure.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(DataContext context) : base(context)
        {
        }

        public async Task<Member> GetMemberByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(m => m.OID == key && m.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            try
            {
                return await QueryAsync(w => w.IsDeleted == false, i => i.UserAccounts, n => n.Teams);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}