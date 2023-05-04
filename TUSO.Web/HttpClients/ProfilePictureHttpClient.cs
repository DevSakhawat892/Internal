using Newtonsoft.Json;
using System.Text;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

namespace TUSO.Web.HttpClients
{
   public class ProfilePictureHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";
      public ProfilePictureHttpClient(HttpClient client)
      {
         this.client = client;
      }

      public async Task<ProfilePicture> Add(ProfilePicture profilePicture)
      {

         var data = JsonConvert.SerializeObject(profilePicture);
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");

         var response = await client.PostAsync($"{BaseUrl}profile-picture", httpContent);

         if (!response.IsSuccessStatusCode)
         {
            //return new RecoveryRequestDto();
            return null;
         }

         string result = await response.Content.ReadAsStringAsync();
         var profilePictureEntry = JsonConvert.DeserializeObject<ProfilePicture>(result);
         return profilePictureEntry;
      }
   }
}
