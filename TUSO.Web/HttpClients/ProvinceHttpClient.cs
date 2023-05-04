using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TUSO.Domain.Entities;

/*
 * Created by: Sakhawat
 * Date created: 11.09.2022
 * Last modified: 11.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.HttpClients
{
   public class ProvinceHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      /// <summary>
      /// Default Constructor
      /// </summary>
      /// <param name="client"></param>
      public ProvinceHttpClient(HttpClient client)
      {
         this.client = client;
      }

      /// <summary>
      /// Creates a row in the table.
      /// </summary>
      /// <param name="entity">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      public async Task<Province> Add(Province province)
      {
         if (province != null)
         {
            if (province.OID == 0)
            {
               var data = JsonConvert.SerializeObject(province);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PostAsync($"{BaseUrl}province", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new Province();
               }
               string result = await response.Content.ReadAsStringAsync();
               var provinceEntry = JsonConvert.DeserializeObject<Province>(result);
               return provinceEntry;
            }
            else
            {
               return new Province();
            }
         }
         else
         {
            return new Province();
         }
      }

      /// <summary>
      /// CountryList for dropdown
      /// </summary>
      /// <returns></returns>
      public async Task<List<Country>> ReadCountries()
      {
         var response = await client.GetAsync($"{BaseUrl}countries");
         if (!response.IsSuccessStatusCode)
         {
            return new List<Country>();
         }
         string result = await response.Content.ReadAsStringAsync();
         var country = JsonConvert.DeserializeObject<List<Country>>(result);
         List<Country> countryList = new List<Country>(country.ToList());
         return countryList;
      }

      /// <summary>
      /// Updates an existing row in the table.
      /// </summary>
      /// <param name="entity">Object to be updated.</param>
      public async Task<Province> Update(Province province)
      {
         if (province != null)
         {
            if (province.OID > 0)
            {
               var data = JsonConvert.SerializeObject(province);
               var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
               var response = await client.PutAsync($"{BaseUrl}province/" + province.OID + "", httpContent);
               if (!response.IsSuccessStatusCode)
               {
                  return new Province();
               }
               string result = await response.Content.ReadAsStringAsync();
               var provinceUpdate = JsonConvert.DeserializeObject<Province>(result);
               return provinceUpdate;
            }
            else
            {
               return new Province();
            }
         }
         else
         {
            return new Province();
         }
      }

      /// <summary>
      /// Deletes a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      public async Task<Province> Delete(int OID)
      {
         var response = await client.DeleteAsync($"{BaseUrl}province" + OID);

         if (!response.IsSuccessStatusCode)
         {
            return new Province();
         }
         string result = await response.Content.ReadAsStringAsync();
         var provinceDelete = JsonConvert.DeserializeObject<Province>(result);
         return provinceDelete;
      }

      //public async Task<Province> FirstOrDefaultAsync(int OID)
      //{
      //   var response = await client.DeleteAsync($"{BaseUrl}province" + OID);

      //   if (!response.IsSuccessStatusCode)
      //   {
      //      return new Province();
      //   }
      //   string result = await response.Content.ReadAsStringAsync();
      //   var provinceDelete = JsonConvert.DeserializeObject<Province>(result);
      //   return provinceDelete;
      //}

      ///// <summary>
      ///// Deletes a row from the table.
      ///// </summary>
      ///// <param name="entity">Object to be deleted.</param>
      //public async Task<Province> Delete(Province province)
      //{
      //   var data = JsonConvert.SerializeObject(province);
      //   var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
      //   var response = await client.PostAsync($"{BaseUrl}Province/DeleteProvince", httpContent);

      //   if (!response.IsSuccessStatusCode)
      //   {
      //      return new Province();
      //   }
      //   string result = await response.Content.ReadAsStringAsync();
      //   var provinceDelete = JsonConvert.DeserializeObject<Province>(result);
      //   return provinceDelete;
      //}
   }
}
