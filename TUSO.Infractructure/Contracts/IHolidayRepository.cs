using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IHolidayRepository : IRepository<Holiday>
    {
        /// <summary>
        /// Returns a holiday if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table Holidays</param>
        /// <returns>Instance of a Holiday object.</returns>
        public Task<Holiday> GetHolidayByKey(int OID);

        /// <summary>
        /// Returns a holiday if the name matched.
        /// </summary>
        /// <param name="holidayName">Name of the holiday.</param>
        /// <returns>Instance of a Holiday object.</returns>
        public Task<Holiday> GetHolidayByName(string holidayName);

        /// <summary>
        /// Returns all holiday.
        /// </summary>
        /// <returns>List of Holiday object.</returns>
        public Task<IEnumerable<Holiday>> GetHolidays();
    }
}