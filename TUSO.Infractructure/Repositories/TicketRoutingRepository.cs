using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Emon
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Repositories
{
    public class TicketRoutingRepository : Repository<TicketRouting>, ITicketRoutingRepository
    {
        public TicketRoutingRepository(DataContext context) : base(context)
        {

        }

        public async Task<TicketRouting> GetTicketRoutingByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.OID == OID && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<TicketRouting>> GetTicketRoutings()
        {
            try
            {
                return await QueryAsync(i => i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}