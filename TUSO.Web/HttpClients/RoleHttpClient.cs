using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
   public class RoleHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <param name="client"></param>
      public RoleHttpClient(HttpClient client)
      {
         this.client = client;
      }

      /// <summary>
      /// Create a row in the table.
      /// </summary>
      /// <param name="designation">Object to be saved in the table as a row</param>
      /// <returns>Saved Object.</returns>
      public async Task<Role> Add(Role role)
      {
         if (role != null)
         {
            if (role.OID == 0)
            {
               var data = JsonConvert.SerializeObject(role);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PostAsync($"{BaseUrl}user-role", httpContent);

               if (!response.IsSuccessStatusCode)
               {
                  return new Role();
               }
               string result = await response.Content.ReadAsStringAsync();
               var roleEntry = JsonConvert.DeserializeObject<Role>(result);
               return roleEntry;
            }
            else
            {
               return new Role();
            }
         }
         else
         {
            return new Role();
         }
      }

      /// <summary>
      /// List for dropdown
      /// </summary>
      /// <returns></returns>
      public async Task<List<Role>> ReadDepartments()
      {
         var response = await client.GetAsync($"{BaseUrl}user-roles");
         if (!response.IsSuccessStatusCode)
         {
            return new List<Role>();
         }
         var result = await response.Content.ReadAsStringAsync();
         var role = JsonConvert.DeserializeObject<List<Role>>(result);
         List<Role> roles = new List<Role>(role.ToList());
         return roles;
      }

      public async Task<Role> Update(Role role)
      {
         if (role != null)
         {
            if (role.OID > 0)
            {
               var data = JsonConvert.SerializeObject(role);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PutAsync($"{BaseUrl}user-role/" + role.OID + "", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new Role();
               }
               string result = await response.Content.ReadAsStringAsync();
               var roleUpdate = JsonConvert.DeserializeObject<Role>(result);
               return roleUpdate;
            }
            else
            {
               return new Role();
            }
         }
         else
         {
            return new Role();
         }
      }
      /// <summary>
      /// Deletes a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      public async Task<Role> Delete(int OID)
      {
         var response = await client.DeleteAsync($"{BaseUrl}user-role/" + OID);

         if (!response.IsSuccessStatusCode)
         {
            return new Role();
         }
         string result = await response.Content.ReadAsStringAsync();
         var roleDelete = JsonConvert.DeserializeObject<Role>(result);
         return roleDelete;
      }
   }
}