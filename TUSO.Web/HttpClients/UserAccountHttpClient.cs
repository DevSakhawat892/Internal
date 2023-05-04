using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 13.09.2022
 * Last modified: 24.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.HttpClients
{
    public class UserAccountHttpClient
    {
        private readonly HttpClient client;
        private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

        public UserAccountHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserAccountDto> Add(UserAccountDto userAccount)
        {
            var data = JsonConvert.SerializeObject(userAccount);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}user-account", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new UserAccountDto();
            }

            string result = await response.Content.ReadAsStringAsync();
            var addUser = JsonConvert.DeserializeObject<UserAccountDto>(result);
            return addUser;
        }

        public async Task<List<UserAccountDto>> ReadUserAccounts()
        {
            var response = await client.GetAsync($"{BaseUrl}user-accounts");

            if (!response.IsSuccessStatusCode)
            {
                return new List<UserAccountDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserAccountDto>>(result);
            List<UserAccountDto> userList = new List<UserAccountDto>(users.ToList());
            return userList;
        }

        public async Task<UserAccountDto> ReadUserAccountByKey(long oid)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}user-account/key/" + oid + "");

            if (!response.IsSuccessStatusCode)
            {
                return new UserAccountDto();
            }
            string result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<UserAccountDto>(result);
            return users;
        }

        public async Task<List<Role>> ReadRoles()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}user-roles");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Role>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Role>>(result) ?? new List<Role>();
        }

        public async Task<List<Country>> ReadCountries()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}countries");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Country>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Country>>(result) ?? new List<Country>();
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

        public async Task<List<Facility>> ReadFacilities()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}facilities");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Facility>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Facility>>(result) ?? new List<Facility>();
        }

        public async Task<List<Department>> ReadDepartments()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}departments");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Department>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Department>>(result) ?? new List<Department>();
        }

        public async Task<List<Designation>> ReadDesignations()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}designations");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Designation>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Designation>>(result) ?? new List<Designation>();
        }

        public async Task<UserAccount> Update(UserAccount userAccount)
        {
            var data = JsonConvert.SerializeObject(userAccount);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}user-account/" + userAccount.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new UserAccount();
            }

            string result = await response.Content.ReadAsStringAsync();
            var updateUser = JsonConvert.DeserializeObject<UserAccount>(result);
            return updateUser;
        }

        public async Task<RecoveryRequestDto> RecoveryRequest(RecoveryRequestDto recoveryRequestDto)
        {
            var data = JsonConvert.SerializeObject(recoveryRequestDto);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}user-account/recovery-request", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new RecoveryRequestDto();
            }

            string result = await response.Content.ReadAsStringAsync();
            var changePassword = JsonConvert.DeserializeObject<RecoveryRequestDto>(result);
            return changePassword;
        }

        public async Task<UserAccountDto> Delete(int OID)
        {
            var response = await client.DeleteAsync($"{BaseUrl}user-account/" + OID);

            if (!response.IsSuccessStatusCode)
            {
                return new UserAccountDto();
            }

            string result = await response.Content.ReadAsStringAsync();
            var deleteUser = JsonConvert.DeserializeObject<UserAccountDto>(result);
            return deleteUser;
        }

        public async Task<UserDetailDto> GetUserDetails(long userId)
        {
            var details = new UserDetailDto
            {
                OID = userId,
            };
            var response = await client.GetAsync($"{BaseUrl}user-account/details/" + userId + "");
            if (!response.IsSuccessStatusCode)
            {
                return new UserDetailDto();
            }
            string result = await response.Content.ReadAsStringAsync();
            var userDetail = JsonConvert.DeserializeObject<UserDetailDto>(result);
            return userDetail;
        }
    }
}