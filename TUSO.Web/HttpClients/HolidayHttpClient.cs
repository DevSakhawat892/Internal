using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022, 21.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.HttpClients
{
    public class HolidayHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public HolidayHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Holiday> Add(Holiday holiday)
        {
            var data = JsonConvert.SerializeObject(holiday);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}holiday", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Holiday();
            }

            string result = await response.Content.ReadAsStringAsync();
            var addDistrict = JsonConvert.DeserializeObject<Holiday>(result);
            return addDistrict;
        }

        public async Task<List<Holiday>> ReadHolidays()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}holidays");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Holiday>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Holiday>>(result) ?? new List<Holiday>();
        }

        public async Task<Holiday> Update(Holiday holiday)
        {
            var data = JsonConvert.SerializeObject(holiday);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}holiday/" + holiday.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Holiday();
            }

            string result = await response.Content.ReadAsStringAsync();
            var updateDistrict = JsonConvert.DeserializeObject<Holiday>(result);
            return updateDistrict;
        }

        public async Task<Holiday> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}holiday/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new Holiday();
            }

            string result = await response.Content.ReadAsStringAsync();
            var deleteHoliday = JsonConvert.DeserializeObject<Holiday>(result);
            return deleteHoliday;
        }
    }
}