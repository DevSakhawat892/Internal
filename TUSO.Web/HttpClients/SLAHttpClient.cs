using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 26.09.2022
 * Last modified: 26.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.HttpClients
{
   public class SLAHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <returns></returns>
      public SLAHttpClient(HttpClient client)
      {
         this.client = client;
      }

      /// <summary>
      /// Create a row in the table.
      /// </summary>
      /// <param name="sLA">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      public async Task<SLA> Add(SLA sLA)
      {
         if (sLA != null)
         {
            if (sLA.OID == 0)
            {
               var data = JsonConvert.SerializeObject(sLA);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PostAsync($"{BaseUrl}sla", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new SLA();
               }
               string result = await response.Content.ReadAsStringAsync();
               var slaEntry = JsonConvert.DeserializeObject<SLA>(result);
               return slaEntry;
            }
            else
            {
               return new SLA();
            }
         }
         else
         {
            return new SLA();
         }
      }

      /// <summary>
      /// List of SLAs
      /// </summary>
      /// <returns></returns>
      public async Task<List<SLA>> ReadSLAs()
      {
         var response = await client.GetAsync($"{BaseUrl}slas");
         if (!response.IsSuccessStatusCode)
         {
            return new List<SLA>();
         }
         string result = await response.Content.ReadAsStringAsync();
         var sla = JsonConvert.DeserializeObject<List<SLA>>(result);
         List<SLA> slaList = new List<SLA>(sla);
         return slaList;
      }

      /// <summary>
      /// Updates an existing row in the table.
      /// </summary>
      /// <param name="entity">Object to be updated.</param>
      public async Task<SLA> Update(SLA sLA)
      {
         if (sLA != null)
         {
            if (sLA.OID > 0)
            {
               var data = JsonConvert.SerializeObject(sLA);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PutAsync($"{BaseUrl}sla/" + sLA.OID + "", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new SLA();
               }
               string result = await response.Content.ReadAsStringAsync();
               var slaUpdate = JsonConvert.DeserializeObject<SLA>(result);
               return slaUpdate;
            }
            else
            {
               return new SLA();
            }
         }
         else
         {
            return new SLA();
         }
      }

      /// <summary>
      /// Deletes a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      public async Task<SLA> Delete(int OID)
      {
         var response = await client.DeleteAsync($"{BaseUrl}sla" + OID);

         if (!response.IsSuccessStatusCode)
         {
            return new SLA();
         }
         string result = await response.Content.ReadAsStringAsync();
         var slaDelete = JsonConvert.DeserializeObject<SLA>(result);
         return slaDelete;
      }
   }
}
