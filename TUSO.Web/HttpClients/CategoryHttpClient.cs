using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 18.09.2022
 * Last modified: 18.09.2022
 * Modified by: Emon
 */
namespace TUSO.Web.HttpClients
{
    public class CategoryHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="client"></param>
        public CategoryHttpClient(HttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Create a row in the table.
        /// </summary>
        /// <param name="category">Object to be saved in the table as a row</param>
        /// <returns>Saved Object.</returns>
        public async Task<IncidentType> Add(IncidentType category)
        {
            if (category != null)
            {
                if (category.OID == 0)
                {
                    var data = JsonConvert.SerializeObject(category);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}incident-type", httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new IncidentType();
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    var categoryEntry = JsonConvert.DeserializeObject<IncidentType>(result);
                    return categoryEntry;
                }
                else
                {
                    return new IncidentType();
                }
            }
            else
            {
                return new IncidentType();
            }
        }

        public async Task<IncidentType> Update(IncidentType category)
        {
            if (category != null)
            {
                if (category.OID > 0)
                {
                    var data = JsonConvert.SerializeObject(category);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{BaseUrl}incident-type/" + category.OID + "", httpContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new IncidentType();
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    var categoryUpdate = JsonConvert.DeserializeObject<IncidentType>(result);
                    return categoryUpdate;
                }
                else
                {
                    return new IncidentType();
                }
            }
            else
            {
                return new IncidentType();
            }
        }
        /// <summary>
        /// Deletes a row from the table.
        /// </summary>
        /// <param name="entity">Object to be deleted.</param>
        public async Task<IncidentType> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}incident-type/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new IncidentType();
            }
            string result = await response.Content.ReadAsStringAsync();
            var categoryDelete = JsonConvert.DeserializeObject<IncidentType>(result);
            return categoryDelete;
        }
    }
}