using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
   public class ModuleHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      public ModuleHttpClient(HttpClient client)
      {
         this.client = client;
      }

      public async Task<Module> Add(Module module)
      {
         var data = JsonConvert.SerializeObject(module);
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");

         var response = await client.PostAsync($"{BaseUrl}module", httpContent);

         if (!response.IsSuccessStatusCode)
         {
            return new Module();
         }

         string result = await response.Content.ReadAsStringAsync();
         var moduleEntry = JsonConvert.DeserializeObject<Module>(result);
         return moduleEntry;
      }


        public async Task<Module> Update(Module module)
        {
            var data = JsonConvert.SerializeObject(module);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}module/" + module.OID, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return new Module();
            }

            string result = await response.Content.ReadAsStringAsync();
            var moduleUpdate = JsonConvert.DeserializeObject<Module>(result);
            return moduleUpdate;
        }
    }
}