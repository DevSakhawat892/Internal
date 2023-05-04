using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

/*
 * Created by: Emon
 * Date created: 19.09.2022
 * Last modified: 19.09.2022
 * Modified by: Emon
 */

namespace TUSO.Web.HttpClients
{
    public class RecoveryRequestHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public RecoveryRequestHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<RecoveryRequestDto> Add(RecoveryRequestDto recoveryRequest)
        {

            var data = JsonConvert.SerializeObject(recoveryRequest);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{BaseUrl}recovery-request", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                //return new RecoveryRequestDto();
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var recoveryRequestEntry = JsonConvert.DeserializeObject<RecoveryRequestDto>(result);
            return recoveryRequestEntry;
        }

        public async Task<RecoveryRequestDto> Update(RecoveryRequestDto recoveryRequest)
        {
            var data = JsonConvert.SerializeObject(recoveryRequest);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}recovery-request/" + recoveryRequest.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new RecoveryRequestDto();
            }

            string result = await response.Content.ReadAsStringAsync();
            var recoveryRequestUpdate = JsonConvert.DeserializeObject<RecoveryRequestDto>(result);
            return recoveryRequestUpdate;
        }

        public async Task<RecoveryRequest> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}recovery-request/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new RecoveryRequest();
            }
            string result = await response.Content.ReadAsStringAsync();
            var recoveryRequestDelete = JsonConvert.DeserializeObject<RecoveryRequest>(result);
            return recoveryRequestDelete;
        }
    }
}