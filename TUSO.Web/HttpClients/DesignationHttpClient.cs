using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
   public class DesignationHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <param name="client"></param>
      public DesignationHttpClient(HttpClient client)
      {
         this.client = client;
      }

      /// <summary>
      /// Create a row in the table.
      /// </summary>
      /// <param name="designation">Object to be saved in the table as a row</param>
      /// <returns>Saved Object.</returns>
      public async Task<Designation> Add(Designation designation)
      {
         if (designation != null)
         {
            if (designation.OID == 0)
            { 
               var data = JsonConvert.SerializeObject(designation);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PostAsync($"{BaseUrl}designation", httpContent);

               if (!response.IsSuccessStatusCode)
               {
                  return new Designation();
               }
               string result = await response.Content.ReadAsStringAsync();
               var designationEntry = JsonConvert.DeserializeObject<Designation>(result);
               return designationEntry;
            }
            else
            {
               return new Designation();
            }
         }
         else
         {
            return new Designation();
         }
      }

      /// <summary>
      /// List for dropdown
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

      public async Task<Designation> Update(Designation designation)
      {
         if (designation != null)
         {
            if (designation.OID > 0)
            {
               var data = JsonConvert.SerializeObject(designation);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PutAsync($"{BaseUrl}designation/" + designation.OID + "", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new Designation();
               }
               string result = await response.Content.ReadAsStringAsync();
               var designationUpdate = JsonConvert.DeserializeObject<Designation>(result);
               return designationUpdate;
            }
            else
            {
               return new Designation();
            }
         }
         else
         {
            return new Designation();
         }
      }
      /// <summary>
      /// Deletes a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      public async Task<Designation> Delete(int OID)
      {
         var response = await client.DeleteAsync($"{BaseUrl}designation" + OID);

         if (!response.IsSuccessStatusCode)
         {
            return new Designation();
         }
         string result = await response.Content.ReadAsStringAsync();
         var designationDelete = JsonConvert.DeserializeObject<Designation>(result);
         return designationDelete;
      }
   }
}
