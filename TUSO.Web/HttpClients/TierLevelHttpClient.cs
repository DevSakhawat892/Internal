using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
   public class TierLevelHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <param name="client"></param>
      public TierLevelHttpClient(HttpClient client)
      {
         this.client = client;
      }

      /// <summary>
      /// Create a row in the table.
      /// </summary>
      /// <param name="designation">Object to be saved in the table as a row</param>
      /// <returns>Saved Object.</returns>
      public async Task<TierLevel> Add(TierLevel tierLevel)
      {
         if (tierLevel != null)
         {
            if (tierLevel.OID == 0)
            {
               var data = JsonConvert.SerializeObject(tierLevel);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PostAsync($"{BaseUrl}tier-level", httpContent);

               if (!response.IsSuccessStatusCode)
               {
                  return new TierLevel();
               }
               string result = await response.Content.ReadAsStringAsync();
               var tierLevelEntry = JsonConvert.DeserializeObject<TierLevel>(result);
               return tierLevelEntry;
            }
            else
            {
               return new TierLevel();
            }
         }
         else
         {
            return new TierLevel();
         }
      }

      /// <summary>
      /// List for dropdown
      /// </summary>
      /// <returns></returns>
      public async Task<List<TierLevel>> ReadTierLevels()
      {
         var response = await client.GetAsync($"{BaseUrl}tier-levels");
         if (!response.IsSuccessStatusCode)
         {
            return new List<TierLevel>();
         }
         var result = await response.Content.ReadAsStringAsync();
         var tierLevel = JsonConvert.DeserializeObject<List<TierLevel>>(result);
         List<TierLevel> departments = new List<TierLevel>(tierLevel.ToList());
         return departments;
      }

      /// <summary>
      /// Update row in the table.
      /// </summary>
      /// <param name="tierLevel"></param>
      /// <returns></returns>
      public async Task<TierLevel> Update(TierLevel tierLevel)
      {
         if (tierLevel != null)
         {
            if (tierLevel.OID > 0)
            {
               var data = JsonConvert.SerializeObject(tierLevel);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PutAsync($"{BaseUrl}tier-level/" + tierLevel.OID + "", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new TierLevel();
               }
               string result = await response.Content.ReadAsStringAsync();
               var tierLevelUpdate = JsonConvert.DeserializeObject<TierLevel>(result);
               return tierLevelUpdate;
            }
            else
            {
               return new TierLevel();
            }
         }
         else
         {
            return new TierLevel();
         }
      }

      /// <summary>
      /// Delete a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      public async Task<TierLevel> Delete(int OID)
      {
         var response = await client.DeleteAsync($"{BaseUrl}tier-level/" + OID);

         if (!response.IsSuccessStatusCode)
         {
            return new TierLevel();
         }

         string result = await response.Content.ReadAsStringAsync();
         var deleteTierLevel = JsonConvert.DeserializeObject<TierLevel> (result);
         return deleteTierLevel;
      }

      /// <summary>
      /// Department Dropdown
      /// </summary>
      /// <returns></returns>
      public async Task<List<Department>> ReadDepartments()
      {
         var response = await client.GetAsync($"{BaseUrl}departments");
         if (!response.IsSuccessStatusCode)
         {
            return new List<Department>();
         }
         var result = await response.Content.ReadAsStringAsync();
         var department = JsonConvert.DeserializeObject<List<Department>>(result);
         List<Department> departments = new List<Department>(department.ToList());
         return departments;
      }

      /// <summary>
      /// IncidentType Dropdown
      /// </summary>
      /// <returns></returns>
      public async Task<List<IncidentType>> ReadIncidentTypes()
      {
         var response = await client.GetAsync($"{BaseUrl}incident-types");
         if (!response.IsSuccessStatusCode)
         {
            return new List<IncidentType>();
         }
         var result = await response.Content.ReadAsStringAsync();
         var incidentType = JsonConvert.DeserializeObject<List<IncidentType>>(result);
         List<IncidentType> incidentTypes = new List<IncidentType>(incidentType.ToList());
         return incidentTypes;
      }
   }
}
