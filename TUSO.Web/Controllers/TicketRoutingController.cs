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
   public class TicketRoutingController : Controller
   {
      private readonly HttpClient httpClient;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public TicketRoutingController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public IActionResult Index()
      {
         var holiday = new HolidayHttpClient(httpClient).ReadHolidays();
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
      public async Task<IActionResult> Index(TicketRouting ticketRouting)
      {
         if (ticketRouting != null)
         {
            if (ticketRouting.OID == 0)
            {
               var ticketRoutingAdded = await new TicketRoutingHttpClient(httpClient).Add(ticketRouting);

               if (ticketRoutingAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var ticketRoutingUpdate = await new TicketRoutingHttpClient(httpClient).Update(ticketRouting);

               if (ticketRoutingUpdate == null)
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
         await new TicketRoutingHttpClient(httpClient).Delete(oid);
         return RedirectToAction("Index");
      }
   }
}
