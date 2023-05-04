using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 11.09.2022
 * Last modified: 12.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.HttpClients
{
    public class DistrictHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public DistrictHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<District> Add(District district)
        {
            var data = JsonConvert.SerializeObject(district);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}district", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new District();
            }

            string result = await response.Content.ReadAsStringAsync();
            var addDistrict = JsonConvert.DeserializeObject<District>(result);
            return addDistrict;
        }

        public async Task<List<Province>> ReadProvinces()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}provinces");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Province>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Province>>(result) ?? new List<Province>();
        }

        public async Task<District> Update(District district)
        {
            var data = JsonConvert.SerializeObject(district);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}district/" + district.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new District();
            }

            string result = await response.Content.ReadAsStringAsync();
            var updateDistrict = JsonConvert.DeserializeObject<District>(result);
            return updateDistrict;
        }

        public async Task<District> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}district/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new District();
            }

            string result = await response.Content.ReadAsStringAsync();
            var deleteDistrict = JsonConvert.DeserializeObject<District>(result);
            return deleteDistrict;
        }
    }
}