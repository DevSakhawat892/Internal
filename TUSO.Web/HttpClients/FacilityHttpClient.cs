using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 13.09.2022
 * Last modified: 13.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.HttpClients
{
    public class FacilityHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public FacilityHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Facility> Add(Facility facility)
        {
            var data = JsonConvert.SerializeObject(facility);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}facility", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Facility();
            }

            string result = await response.Content.ReadAsStringAsync();
            var addFacility = JsonConvert.DeserializeObject<Facility>(result);
            return addFacility;
        }

        public async Task<List<District>> ReadDistricts()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}districts");

            if (!response.IsSuccessStatusCode)
            {
                return new List<District>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<District>>(result) ?? new List<District>();
        }

        public async Task<Facility> Update(Facility facility)
        {
            var data = JsonConvert.SerializeObject(facility);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}facility/" + facility.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Facility();
            }

            string result = await response.Content.ReadAsStringAsync();
            var updateDistrict = JsonConvert.DeserializeObject<Facility>(result);
            return updateDistrict;
        }

        public async Task<Facility> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}facility/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new Facility();
            }

            string result = await response.Content.ReadAsStringAsync();
            var deleteFacility = JsonConvert.DeserializeObject<Facility>(result);
            return deleteFacility;
        }
    }
}