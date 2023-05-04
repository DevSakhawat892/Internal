using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
    public class CountryHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public CountryHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Country> Add(Country country)
        {
            
            var data = JsonConvert.SerializeObject(country);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
         
            var response = await client.PostAsync($"{BaseUrl}country", httpContent);
            
            if (!response.IsSuccessStatusCode)
            {
                return new Country();
            }

            string result = await response.Content.ReadAsStringAsync();
            var countryEntry = JsonConvert.DeserializeObject<Country>(result);
            return countryEntry;           
        }

        public async Task<Country> Update(Country country)
        {          
            var data = JsonConvert.SerializeObject(country);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}country/" + country.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Country();
            }

            string result = await response.Content.ReadAsStringAsync();
            var countryUpdate = JsonConvert.DeserializeObject<Country>(result);
            return countryUpdate;          
        }

        public async Task<Country> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}country/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new Country();
            }
            string result = await response.Content.ReadAsStringAsync();
            var countryDelete = JsonConvert.DeserializeObject<Country>(result);
            return countryDelete;
        }
    }
}