using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022, 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class HolidayListRepository : Repository<HolidayList>, IHolidayListRepository
    {

        public HolidayListRepository(DataContext context) : base(context)
        {

        }

        public async Task<HolidayList> GetHolidayListByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.OID == OID && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HolidayList> GetHolidayListByHoliday(int HolidayID)
        {
            try
            {
                return await FirstOrDefaultAsync(g => g.HolidayID == HolidayID && g.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<HolidayList>> GetHolidayLists()
        {
            try
            {
                return await QueryAsync(c => c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string PostAllHolidayList(HolidayList holidayList)
        {
            int day = Convert.ToInt32(holidayList.DayName);
            var dayname = (DayOfWeek)day;
            var date = context.Holidays.Where(h => h.OID == holidayList.HolidayID).FirstOrDefault();
            var from = date.StartDate;
            var to = date.EndDate;
            var allHoliday = GetWeekDayInRange(from, to, dayname);
           
            HolidayList holiday = new HolidayList();
            foreach(var h in allHoliday)
            {
                holiday.OID = 0;
                holiday.HolidayID = holidayList.HolidayID;
                holiday.DayName = dayname.ToString();
                holiday.Holiday = h;
                holiday.Discription = "Weekend";
                context.HolidayLists.Add(holiday);
                context.SaveChanges();
            }
            return "";
        }

        public List<DateTime> GetWeekDayInRange(DateTime from, DateTime to, DayOfWeek day)
        {
            var dates = new List<DateTime>();
            for (var dt = from; dt <= to; dt = dt.AddDays(1))
                dates.Add(dt);

            var filteredDates = dates.Where(x => x.DayOfWeek == day).ToList();
            return filteredDates;
        }

        public string GetVacation(HolidayList holidayList, DateTime froms, DateTime tos)
        {
            HolidayList holiday = new HolidayList();
            for (var day = froms.Date; day.Date <= tos.Date; day = day.AddDays(1))
            {
                holiday.OID = 0;
                holiday.HolidayID = holidayList.HolidayID;
                holiday.DayName = (day.DayOfWeek).ToString();
                holiday.Discription = holidayList.Discription;
                holiday.Holiday = day;
                context.HolidayLists.Add(holiday);
                context.SaveChanges();
            }
            return "";
        }
    }
}