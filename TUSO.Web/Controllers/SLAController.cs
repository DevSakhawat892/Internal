using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Sakhawat
 * Date created: 26.09.2022
 * Last modified: 26.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.Controllers
{
   public class SLAController : Controller
   {
      private readonly HttpClient httpClient;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public SLAController(HttpClient httpClient)
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
         var holiday = await new HolidayHttpClient(httpClient).ReadHolidays();
         ViewBag.holiday = holiday;

         return View();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sLA">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Index(SLA sLA)
      {
         if (sLA != null)
         {
            if (sLA.OID == 0)
            {

               var slaAdded = await new SLAHttpClient(httpClient).Add(sLA);

               if (slaAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var slaUpdate = await new SLAHttpClient(httpClient).Update(sLA);

               if (slaUpdate == null)
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
      /// URL: tuso-api/sla
      /// </summary>
      /// <param name="id">Primary key of the table</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      public async Task<IActionResult> Delete(int oid)
      {
         await new SLAHttpClient(httpClient).Delete(oid);
         return RedirectToAction("Index");
      }
   }
}
