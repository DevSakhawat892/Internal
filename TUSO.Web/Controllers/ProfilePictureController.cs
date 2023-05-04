using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

namespace TUSO.Web.Controllers
{
   public class ProfilePictureController : Controller
   {
      private readonly HttpClient client;
      private readonly IWebHostEnvironment webHost;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public ProfilePictureController(HttpClient client, IWebHostEnvironment webHost)
      {
         this.client = client;
         this.webHost = webHost;
      }

      [HttpGet]
      public IActionResult Index()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Index(ProfilePicture profilePicture, IFormFile file)
      {
         if (file != null)
         {
            //To save a image to a folder
            string webRootPath = webHost.WebRootPath;
            string contentRootPath = webHost.ContentRootPath;

            string path = "";
            //or path = Path.Combine(webRootPath, "Image");
            path = Path.Combine(contentRootPath , "wwwroot" , "Image");

            //string picture = System.IO.Path.GetFileName(file.FileName);
            //string path = System.IO.Path.Combine(Server.MapPath("~/Images"), picture);
            //file.SaveAs(path);

            //To store as byte[] in a Table of Database
            using (MemoryStream ms = new MemoryStream())
            {
               await file.CopyToAsync(ms);
               profilePicture.ProfilePictures = ms.GetBuffer();
            }
         }
         else
         {
            return RedirectToAction("Index", "UserAccount");
         }

         var profilePictureAdd = await new ProfilePictureHttpClient(client).Add(profilePicture);

         if (profilePictureAdd == null)
         {
            return RedirectToAction("Index", "UserAccount");
         }
         return RedirectToAction("Index", "UserAccount");

         //byte[] bytes = Convert.FromBase64String(profilePicture.ProfilePictures);

         //{
         //   //convert uploaded image as image object like given below
         //   profilePicture.ProfilePictures image = profilePicture.ProfilePictures.FromStream(file.InputStream, true, true);
         //   //call 'ImageToBase64' function here
         //   string base64String = ImageToBase64(image, System.Drawing.Imaging.ImageFormat.Jpeg);
         //   //'System.Drawing.Imaging.ImageFormat.Jpeg' is the image extension
         //   return View();

         //}

         //using (profilePicture.ProfilePictures image = profilePicture.ProfilePictures(Path))
         //{
         //   using (MemoryStream m = new MemoryStream())
         //   {
         //      image.Save(m, image.RawFormat);
         //      byte[] imageBytes = m.ToArray();

         //      // Convert byte[] to Base64 String
         //      string base64String = Convert.ToBase64String(imageBytes);
         //      return base64String;
         //   }
         //}
      }
   }
}
