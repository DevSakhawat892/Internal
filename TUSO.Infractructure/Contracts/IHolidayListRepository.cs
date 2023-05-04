using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022, 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface IHolidayListRepository : IRepository<HolidayList>
    {

        /// <summary>
        /// Returns a holidayList if key matched.
        /// </summary>
        /// <param name="key">Primary key of the table HolidayLists</param>
        /// <returns>Instance of a HolidayList object.</returns>
        public Task<HolidayList> GetHolidayListByKey(int OID);

        /// <summary>
        /// Returns a holiday list if HolidayID matched
        /// </summary>
        /// <param name="HolidayID"Primary key of the holiday table ></param>
        /// <returns></returns>
        public Task<HolidayList> GetHolidayListByHoliday(int HolidayID);

        /// <summary>
        /// Returns all holidayList.
        /// </summary>
        /// <returns>List of HolidayList object.</returns>
        public Task<IEnumerable<HolidayList>> GetHolidayLists();

        string PostAllHolidayList(HolidayList holidayList);

        public string GetVacation(HolidayList holidayList, DateTime froms, DateTime tos);
    }
}