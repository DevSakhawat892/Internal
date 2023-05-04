using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.HttpClients
{
    public class PriorityHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public PriorityHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Priority> Add(Priority priority)
        {
            var data = JsonConvert.SerializeObject(priority);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}prioritoy", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Priority();
            }

            string result = await response.Content.ReadAsStringAsync();
            var addPrioritoy = JsonConvert.DeserializeObject<Priority>(result);
            return addPrioritoy;
        }

        public async Task<List<Priority>> ReadPriorities()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}priorities");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Priority>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Priority>>(result) ?? new List<Priority>();
        }

        public async Task<List<SLA>> ReadSLA()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}slas");

            if (!response.IsSuccessStatusCode)
            {
                return new List<SLA>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SLA>>(result) ?? new List<SLA>();
        }

        public async Task<Priority> Update(Priority priority)
        {
            var data = JsonConvert.SerializeObject(priority);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}prioritoy/" + priority.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Priority();
            }

            string result = await response.Content.ReadAsStringAsync();
            var updatePriority = JsonConvert.DeserializeObject<Priority>(result);
            return updatePriority;
        }

        public async Task<Priority> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}priority/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new Priority();
            }

            string result = await response.Content.ReadAsStringAsync();
            var deletePriority = JsonConvert.DeserializeObject<Priority>(result);
            return deletePriority;
        }
    }
}