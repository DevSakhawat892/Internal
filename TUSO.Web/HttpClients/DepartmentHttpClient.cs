using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 12.09.2022
 * Last modified: 12.09.2022
 * Modified by: Emon
 */

namespace TUSO.Web.HttpClients
{
    public class DepartmentHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="client"></param>
        public DepartmentHttpClient(HttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Create a row in the table.
        /// </summary>
        /// <param name="department">Object to be saved in the table as a row</param>
        /// <returns>Saved Object.</returns>
        public async Task<Department> Add(Department department)
        {
            if (department != null)
            {
                if (department.OID == 0)
                {
                    var data = JsonConvert.SerializeObject(department);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}department", httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Department();
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    var departmentEntry = JsonConvert.DeserializeObject<Department>(result);
                    return departmentEntry;
                }
                else
                {
                    return new Department();
                }
            }
            else
            {
                return new Department();
            }
        }

        public async Task<Department> Update(Department department)
        {
            if (department != null)
            {
                if (department.OID > 0)
                {
                    var data = JsonConvert.SerializeObject(department);
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{BaseUrl}department/" + department.OID + "", httpContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Department();
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    var departmentUpdate = JsonConvert.DeserializeObject<Department>(result);
                    return departmentUpdate;
                }
                else
                {
                    return new Department();
                }
            }
            else
            {
                return new Department();
            }
        }
        /// <summary>
        /// Deletes a row from the table.
        /// </summary>
        /// <param name="entity">Object to be deleted.</param>
        public async Task<Department> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}department/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new Department();
            }
            string result = await response.Content.ReadAsStringAsync();
            var departmentDelete = JsonConvert.DeserializeObject<Department>(result);
            return departmentDelete;
        }
    }
}