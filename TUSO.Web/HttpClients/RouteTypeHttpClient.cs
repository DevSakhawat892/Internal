using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
    public class RouteTypeHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public RouteTypeHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<RouteType> Add(RouteType routeType)
        {

            var data = JsonConvert.SerializeObject(routeType);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{BaseUrl}route-type", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new RouteType();
            }

            string result = await response.Content.ReadAsStringAsync();
            var routetypeEntry = JsonConvert.DeserializeObject<RouteType>(result);
            return routetypeEntry;
        }

        public async Task<RouteType> Update(RouteType routeType)
        {
            var data = JsonConvert.SerializeObject(routeType);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}route-type/" + routeType.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new RouteType();
            }

            string result = await response.Content.ReadAsStringAsync();
            var routeTypeUpdate = JsonConvert.DeserializeObject<RouteType>(result);
            return routeTypeUpdate;
        }

        public async Task<RouteType> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}DeleteRouteType" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new RouteType();
            }
            string result = await response.Content.ReadAsStringAsync();
            var routeTypeDelete = JsonConvert.DeserializeObject<RouteType>(result);
            return routeTypeDelete;
        }
    }
}