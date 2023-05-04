using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
    /*
     * Created by: Rakib
     * Date created: 11.09.2022
     * Last modified: 11.09.2022
     * Modified by: Rakib
     */

    public class HomeHttpClient
    {
        private readonly HttpClient client;
        private readonly static string BaseUrl = "https://localhost:7026/tuso-api/";
        public HomeHttpClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<UserAccount> UserLogin(LoginDto model)
        {
            var data = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync($"{BaseUrl}user-account/login", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                return new UserAccount();
            }
            string result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserAccount>(result);
            return user;
        }
        public async Task<LoginDto> ResetPassword(LoginDto model)
        {
            if (model != null)
            {
                var data = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{BaseUrl}Accounts/ChangePassword", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    return new LoginDto();
                }
                string result = await response.Content.ReadAsStringAsync();
                var changepassword = JsonConvert.DeserializeObject<LoginDto>(result);
                return changepassword;
            }
            return new LoginDto();
        }
    }
}