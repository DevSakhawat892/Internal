﻿using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 05.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class IncidentRepository : Repository<Incident>, IIncidentRepository
    {
        public IncidentRepository(DataContext context) : base(context)
        {
        }

        public async Task<Incident> GetIncidentByKey(long OID)
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

        public async Task<Incident> GetIncidentByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.Title.ToLower().Trim() == name.ToLower().Trim() && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Incident>> GetIncidents()
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