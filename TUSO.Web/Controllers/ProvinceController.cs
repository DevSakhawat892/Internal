using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Sakhawat
 * Date created: 11.09.2022
 * Last modified: 11.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.Controllers
{
   public class ProvinceController : Controller
   {
      private readonly HttpClient httpClient;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public ProvinceController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         var country = await new ProvinceHttpClient(httpClient).ReadCountries();
         ViewBag.Country = country;

         return View();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="province">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(Province province)
      {
         if (province != null)
         {
            if (province.OID == 0)
            {

               var provinceAdded = await new ProvinceHttpClient(httpClient).Add(province);

               if (provinceAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var provinceUpdate = await new ProvinceHttpClient(httpClient).Update(province);

               if (provinceUpdate == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
         }
         else
         {
            return RedirectToAction("Index");
         }
      }

      /// <summary>
      /// URL: tuso-api/province
      /// </summary>
      /// <param name="id">Primary key of the table</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      public async Task<IActionResult> Delete(int oid)
      {
         await new ProvinceHttpClient(httpClient).Delete(oid);
         return RedirectToAction("Index");
      }

      ///// <summary>
      ///// URL: tuso-api/province
      ///// </summary>
      ///// <param name="id">Primary key of the table</param>
      ///// <returns>Deletes a row from the table.</returns>
      //[HttpDelete]
      //public async Task<IActionResult> Delete(int OID)
      //{
      //   var province = await new ProvinceHttpClient(httpClient).FirstOrDefaultAsync(OID);

      //   await new ProvinceHttpClient(httpClient).Delete(province);

      //   return RedirectToAction("Index");
      //}

      ///// <summary>
      ///// URL: tuso-api/province
      ///// </summary>
      ///// <param name="id">Primary key of the talbe</param>
      ///// <returns>Get Single record.</returns>
      //public async Task<IActionResult> Edit(int id)
      //{
      //   var province = await new ProvinceHttpClient(httpClient).FirstOrDefaultAsync(id);

      //   return RedirectToAction("Index", province);
      //}
   }
}