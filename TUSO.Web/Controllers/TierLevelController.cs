using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Sakhawat
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.Controllers
{
   public class TierLevelController : Controller
   {
      private readonly HttpClient httpClient;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public TierLevelController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      /// <summary>
      /// URL: tuso-api/designations
      /// </summary>
      /// <returns>List of designation</returns>
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         var IncidentType = await new TierLevelHttpClient(httpClient).ReadIncidentTypes();
         ViewBag.IncidentType = IncidentType;

         var Department = await new TierLevelHttpClient(httpClient).ReadDepartments();
         ViewBag.Department = Department;

         return View();
      }

      /// <summary>
      /// URL: tuso-api/incident-type
      /// </summary>
      /// <param name="tierLevel">object to be saved in the table as a row</param>
      /// <returns>Saved object</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(TierLevel tierLevel)
      {
         if (tierLevel != null)
         {
            if (tierLevel.OID == 0)
            {
               var tierLevelAdded = await new TierLevelHttpClient(httpClient).Add(tierLevel);

               if (tierLevelAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var tierLevelUpdate = await new TierLevelHttpClient(httpClient).Update(tierLevel);

               if (tierLevelUpdate == null)
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
      /// URL: tuso-api/incident-type
      /// </summary>
      /// <param name="key">Pirmary key of the table</param>
      /// <returns>Delete a row from the table</returns>
      [HttpDelete]
      public async Task<IActionResult> Delete(int OID)
      {
         await new DesignationHttpClient(httpClient).Delete(OID);
         return RedirectToAction("Index");
      }
   }
}