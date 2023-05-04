using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
   public class ApplicationPermissionHttpClient
   {
      private readonly HttpClient client;
      private readonly string Baseurl = "https://localhost:7026/tuso-api/";

      public ApplicationPermissionHttpClient(HttpClient client)
      {
         this.client = client;
      }

      public async Task<List<ApplicationPermission>> ReadApplicationPermissions()
      {
         var response = await client.GetAsync($"{Baseurl}application-permissions");

         if (!response.IsSuccessStatusCode)
         {
            return new List<ApplicationPermission>();
         }

         string result = await response.Content.ReadAsStringAsync();
         var appPermissionList = JsonConvert.DeserializeObject<List<ApplicationPermission>>(result) ?? new List<ApplicationPermission>();
         return appPermissionList;
      }

      public async Task<ApplicationPermission> Add(ApplicationPermission applicationPermission)
      {
         var data = JsonConvert.SerializeObject(applicationPermission);
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
         var response = await client.PostAsync($"{Baseurl}application-permission", httpContent);

         if (!response.IsSuccessStatusCode)
         {
            return new ApplicationPermission();
         }

         string result = await response.Content.ReadAsStringAsync();
         var appPermission = JsonConvert.DeserializeObject<ApplicationPermission>(result);
         return appPermission;
      }

      public async Task<ApplicationPermission> Update(ApplicationPermission applicationPermission)
      {
         var data = JsonConvert.SerializeObject(applicationPermission);
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
         var response = await client.PutAsync($"{Baseurl}application-permission/" + applicationPermission.OID + "", httpContent);

         if (!response.IsSuccessStatusCode)
         {
            return new ApplicationPermission();
         }

         string result = await response.Content.ReadAsStringAsync();
         var appPermissionUpdate = JsonConvert.DeserializeObject<ApplicationPermission>(result);
         return appPermissionUpdate;
      }

      public async Task<List<Module>> ReadModules()
      {
         var response = await client.GetAsync($"{Baseurl}modules");

         if (!response.IsSuccessStatusCode)
         {
            return new List<Module>();
         }

         string result = await response.Content.ReadAsStringAsync();
         var moduleList = JsonConvert.DeserializeObject<List<Module>>(result) ?? new List<Module>();
         return moduleList;
      }
   }
}
