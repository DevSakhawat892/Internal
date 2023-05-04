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
   public class TicketRoutingHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <returns></returns>
      public TicketRoutingHttpClient(HttpClient client)
      {
         this.client = client;
      }

      /// <summary>
      /// Create a row in the table.
      /// </summary>
      /// <param name="ticketRouting">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      public async Task<TicketRouting> Add(TicketRouting ticketRouting)
      {
         if (ticketRouting != null)
         {
            if (ticketRouting.OID == 0)
            {
               var data = JsonConvert.SerializeObject(ticketRouting);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PostAsync($"{BaseUrl}ticket-routing", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new TicketRouting();
               }
               string result = await response.Content.ReadAsStringAsync();
               var ticketRoutingEntry = JsonConvert.DeserializeObject<TicketRouting>(result);
               return ticketRoutingEntry;
            }
            else
            {
               return new TicketRouting();
            }
         }
         else
         {
            return new TicketRouting();
         }
      }

      /// <summary>
      /// List of TicketRoutings
      /// </summary>
      /// <returns></returns>
      public async Task<List<TicketRouting>> ReadTicketRoutings()
      {
         var response = await client.GetAsync($"{BaseUrl}ticket-routings");
         if (!response.IsSuccessStatusCode)
         {
            return new List<TicketRouting>();
         }
         string result = await response.Content.ReadAsStringAsync();
         var ticketRouting = JsonConvert.DeserializeObject<List<TicketRouting>>(result);
         List<TicketRouting> ticketRoutingList = new List<TicketRouting>(ticketRouting);
         return ticketRoutingList;
      }

      /// <summary>
      /// Updates an existing row in the table.
      /// </summary>
      /// <param name="ticketRouting">Object to be updated.</param>
      public async Task<TicketRouting> Update(TicketRouting ticketRouting)
      {
         if (ticketRouting != null)
         {
            if (ticketRouting.OID > 0)
            {
               var data = JsonConvert.SerializeObject(ticketRouting);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PutAsync($"{BaseUrl}ticket-routing/" + ticketRouting.OID + "", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new TicketRouting();
               }
               string result = await response.Content.ReadAsStringAsync();
               var ticketRoutingUpdate = JsonConvert.DeserializeObject<TicketRouting>(result);
               return ticketRoutingUpdate;
            }
            else
            {
               return new TicketRouting();
            }
         }
         else
         {
            return new TicketRouting();
         }
      }

      /// <summary>
      /// Deletes a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      public async Task<TicketRouting> Delete(int OID)
      {
         var response = await client.DeleteAsync($"{BaseUrl}sla" + OID);

         if (!response.IsSuccessStatusCode)
         {
            return new TicketRouting();
         }
         string result = await response.Content.ReadAsStringAsync();
         var ticketRoutingDelete = JsonConvert.DeserializeObject<TicketRouting>(result);
         return ticketRoutingDelete;
      }

      /// <summary>
      /// List of RouteType for dropdown
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
   }
}
