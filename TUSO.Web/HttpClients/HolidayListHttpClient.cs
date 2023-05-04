using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 22.09.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.HttpClients
{
    public class HolidayListHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public HolidayListHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<HolidayList>?> GetAllWeekEnd()
        {
            var response = await client.GetAsync($"{BaseUrl}holidaylists");
            if (!response.IsSuccessStatusCode)
            {
                return new List<HolidayList>();
            }
            string result = await response.Content.ReadAsStringAsync();
            var holiday = JsonConvert.DeserializeObject<List<HolidayList>>(result);
            List<HolidayList> holidayList = new List<HolidayList>(holiday.ToList());

            return holidayList;
        }

        public async Task<string> PostAllWeekEnd(HolidayList? holidayList)
        {
            if (holidayList != null)
            {
                if (holidayList.OID == 0)
                {
                    var hlist = new HolidayList
                    {
                        Holiday = DateTime.Now,
                        DayName = holidayList.DayName,
                        Discription = "Weekend",
                        HolidayID = 1
                    };

                    var data = JsonConvert.SerializeObject(hlist);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}holidaylist-post", httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    var abc = new HolidayList
                    {
                        OID = holidayList.OID,
                        Holiday = DateTime.Now,
                        DayName = ((DayOfWeek)Convert.ToInt32(holidayList.DayName)).ToString(),
                        Discription = "Weekend",
                        HolidayID = 1
                    };
                    var data = JsonConvert.SerializeObject(abc);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{BaseUrl}holidaylist/" + holidayList.OID, httpContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    string result = await response.Content.ReadAsStringAsync();

                    return result;
                }
            }
            else
            {
                return "";
            }
        }

        public async Task<string> PostAllVacation(HolidayList? holiday, DateTime from, DateTime to)
        {
            if (holiday != null)
            {
                if (holiday.OID == 0)
                {
                    var holidayList = new HolidayList();
                    for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                    {
                        holidayList.Holiday = day;
                        holidayList.DayName = (day.DayOfWeek).ToString();
                        holidayList.Discription = holiday.Discription;
                        holidayList.HolidayID = 1;
                    };

                    var data = JsonConvert.SerializeObject(holidayList);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}holidaylist-vacation?froms=" + from + "&tos=" + to + "", httpContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    var data = JsonConvert.SerializeObject(holiday);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{BaseUrl}holidaylist/" + holiday.OID, httpContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            else
            {
                return "";
            }
        }

        public async Task<bool> DeleteWeekEnd(int holidayListID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}holidaylist/" + holidayListID);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            string result = await response.Content.ReadAsStringAsync();
            return bool.Parse(result);
        }
    }
}